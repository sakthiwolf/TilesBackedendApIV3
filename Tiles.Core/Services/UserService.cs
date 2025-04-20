
using Tiles.Core.DTO;
using Tiles.Core.ServiceContracts.UserManagement.Application.Interfaces;
using Tiles.Core.Wrappers;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.RepositroyContracts;

namespace Tiles.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ServiceResult> RegisterUserAsync(UserRequestDto dto)
        {
            try
            {
                var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
                if (existingUser != null)
                    return ServiceResult.CreateFailure("User already exists with this email.");

                var tempPassword = GenerateTemporaryPassword();
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(tempPassword);

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = dto.Email,
                    PasswordHash = hashedPassword,
                    Name = dto.Name,
                    Designation = dto.Designation,
                    Phone = dto.Phone,
                    IsActive = dto.IsActive,
                    IsFirst = false,
                    SerialNumber = await _userRepository.GetNextSerialNumberAsync()
                };

                await _userRepository.AddAsync(user);
                await SendTemporaryPasswordEmail(dto.Email, tempPassword);

                return ServiceResult.CreateSuccess("User registered successfully. A temporary password has been sent to the user's email.");
            }
            catch (Exception ex)
            {
                return ServiceResult.CreateFailure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<ServiceResult<TokenResponseDto>> LoginAsync(LoginDto dto)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(dto.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                    return ServiceResult<TokenResponseDto>.CreateFailure("Invalid email or password.");

                var token = GenerateToken(user);
                return ServiceResult<TokenResponseDto>.CreateSuccess(new TokenResponseDto { Token = token }, "Login successful.");
            }
            catch (Exception ex)
            {
                return ServiceResult<TokenResponseDto>.CreateFailure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<ServiceResult> UpdateUserAsync(Guid id, UserRequestDto dto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    return ServiceResult.CreateFailure("User not found.");

                user.Name = dto.Name;
                user.Designation = dto.Designation;
                user.Phone = dto.Phone;
                user.IsActive = dto.IsActive;

                await _userRepository.UpdateAsync(user);
                return ServiceResult.CreateSuccess("User updated successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResult.CreateFailure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                return user == null ? null : new UserResponseDto
                {
                    Id = user.Id,
                    SerialNumber = user.SerialNumber,
                    Name = user.Name,
                    Designation = user.Designation,
                    Email = user.Email,
                    Phone = user.Phone,
                    IsActive = user.IsActive
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<PaginatedResult<UserResponseDto>> GetUsersAsync(string search, int pageNo, int rowsPerPage)
        {
            try
            {
                var users = await _userRepository.GetUsersAsync(search, pageNo, rowsPerPage);
                var totalRecords = await _userRepository.GetTotalCountAsync(search);

                var userDtos = users.Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    SerialNumber = u.SerialNumber,
                    Name = u.Name,
                    Designation = u.Designation,
                    Email = u.Email,
                    Phone = u.Phone,
                    IsActive = u.IsActive
                }).ToList();

                return PaginatedResult<UserResponseDto>.CreateSuccess(userDtos, pageNo, rowsPerPage, totalRecords);
            }
            catch (Exception ex)
            {
                return PaginatedResult<UserResponseDto>.CreateFailure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<ServiceResult> DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    return ServiceResult.CreateFailure("User not found.");

                await _userRepository.DeleteAsync(user);
                return ServiceResult.CreateSuccess("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResult.CreateFailure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<ServiceResult> UpdatePasswordAsync(UpdatePasswordDto dto)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(dto.Email);
                if (user == null)
                    return ServiceResult.CreateFailure("User not found.");

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                await _userRepository.UpdateAsync(user);

                return ServiceResult.CreateSuccess("Password updated successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResult.CreateFailure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<ServiceResult> ForgotPasswordAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null)
                    return ServiceResult.CreateFailure("User not found.");

                var tempPassword = GenerateTemporaryPassword();
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(tempPassword);
                user.IsFirst = true;

                await _userRepository.UpdateAsync(user);

                var subject = "Your Temporary Password";
                var body = $"Hello {user.Name},\n\nYour temporary password is: {tempPassword}\nPlease log in and change your password.\n\nThank you!";
                await SendEmailAsync(email, subject, body);

                return ServiceResult.CreateSuccess("Temporary password sent to your email.");
            }
            catch (Exception ex)
            {
                return ServiceResult.CreateFailure($"An error occurred: {ex.Message}");
            }
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPortString = _configuration["EmailSettings:Port"];
                var email = _configuration["EmailSettings:Email"];
                var password = _configuration["EmailSettings:Password"];

                if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(smtpPortString)
                    || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    throw new InvalidOperationException("Email settings are not properly configured.");
                }

                if (!int.TryParse(smtpPortString, out var smtpPort))
                {
                    throw new FormatException("Invalid SMTP port configuration.");
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Your App", email));
                message.To.Add(new MailboxAddress("", to)); // blank name is safer for Gmail
                message.Subject = subject;

                // Detect if HTML or plain
                message.Body = new TextPart(isHtml ? "html" : "plain")
                {
                    Text = body
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(email, password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                Console.WriteLine("✅ Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send email: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                throw;
            }
        }


        private string GenerateTemporaryPassword(int length = 10)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#$!";
            var random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        private async Task SendTemporaryPasswordEmail(string userEmail, string temporaryPassword)
        {
            var subject = "Your Temporary Password";
            var body = $@"
        <html>
        <body style='font-family: Arial, sans-serif; color: #333;'>
            <h2>Welcome to Our Service!</h2>
            <p>Your temporary password is: <strong style='color: #2b2b2b;'>{temporaryPassword}</strong></p>
            <p>Please change your password immediately after logging in to keep your account secure.</p>
            <br />
            <p>Thanks,</p>
            <p><em>Your App Team</em></p>
        </body>
        </html>
    ";

            await SendEmailAsync(userEmail, subject, body, isHtml: true);
        }



        private string GenerateToken(User user)
        {
            // Replace this with actual JWT logic
            return "some-jwt-token";
        }

        public Task<ServiceResult> SendOtpAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> VerifyOtpAsync(string email, string otp)
        {
            throw new NotImplementedException();
        }

    
    }
}
