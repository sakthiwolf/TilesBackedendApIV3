using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Core.ServiceContracts.UserManagement.Application.Interfaces;
using Tiles.Core.Wrappers;
using MailKit.Net.Smtp;
using Tiles.Core.DTO.UserDto;
using Tiles.Core.Domain.Entities;
 
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository; 
    private readonly IConfiguration _configuration;

    // Constructor with DI for user repository and configuration
    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    // Registers a new user with a temporary password and sends a welcome email
    public async Task<ServiceResult> RegisterUserAsync(UserRequestDto dto)
    {
        // Check if email is already registered
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            return ServiceResult.CreateFailure("User already exists");

        // Generate and hash temporary password
        var tempPassword = GenerateTemporaryPassword();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(tempPassword);

        // Generate unique serial number
        var serialNumber = await _userRepository.GetNextSerialNumberAsync();

        // Create new user entity
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email,
            Designation = dto.Designation,
            Phone = dto.Phone,
            IsActive = dto.IsActive,
            SerialNumber = serialNumber,
            PasswordHash = hashedPassword,
            IsFirst = true // Indicates first-time login
        };

        await _userRepository.AddAsync(user);

        // Send email only if the user is active
        if (dto.IsActive)
        {
            var htmlContent = $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset=""UTF-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <style>
    body {{
      font-family: Arial, sans-serif;
      background-color: #f4f4f4;
      margin: 0;
      padding: 0;
    }}
    .container {{
      max-width: 600px;
      margin: 40px auto;
      background-color: #ffffff;
      padding: 20px;
      border-radius: 8px;
      box-shadow: 0 0 10px rgba(0,0,0,0.05);
    }}
    .header {{
      text-align: center;
      padding-bottom: 20px;
    }}
    .footer {{
      font-size: 12px;
      color: #888888;
      text-align: center;
      padding-top: 20px;
    }}
    .button {{
      display: inline-block;
      padding: 10px 20px;
      background-color: #007BFF;
      color: #ffffff;
      text-decoration: none;
      border-radius: 5px;
      margin-top: 20px;
    }}
  </style>
</head>
<body>
  <div class=""container"">
    <div class=""header"">
      <h2>Welcome to Endless Enterprise</h2>
    </div>
    <p>Hi {dto.Name},</p>
    <p>We're excited to have you on board! Your account has been created successfully.</p>
    <p><strong>Temporary Password:</strong> {tempPassword}</p>
    <p>We recommend you log in and change your password as soon as possible.</p>
    <p>
      <a href=""https://localhost:5173"" class=""button"">Log In Now</a>
    </p>
    <p>If you have any questions, feel free to contact our support team.</p>
    <div class=""footer"">
      <p>&copy; {DateTime.UtcNow.Year} Your Company. All rights reserved.</p>
    </div>
  </div>
</body>
</html>";

            var plainTextContent = $@"
Hi {dto.Name},

Welcome to Our Platform!

Your temporary password is: {tempPassword}

Please visit https://localhost:5173 to log in and set your new password.

Thank you,
Your Company Team
";

            await SendEmailAsync(
                toEmail: dto.Email,
                toName: dto.Name,
                subject: "Welcome! Set up your password",
                htmlBody: htmlContent
            );
        }

        return ServiceResult.CreateSuccess("User registered successfully");
    }



    // Authenticates user and generates token (stubbed)
    public async Task<ServiceResult<TokenResponseDto>> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return ServiceResult<TokenResponseDto>.CreateFailure("Invalid email or password.");

        // Generate a JWT token (stubbed for now)
        var token = GenerateToken(user);

        var userDto = new UserResponseDto
        {
            Id = user.Id,
            SerialNumber = user.SerialNumber,
            Name = user.Name,
            Email = user.Email,
            Designation = user.Designation,
            Phone = user.Phone,
            IsActive = user.IsActive
        };

        return ServiceResult<TokenResponseDto>.CreateSuccess(new TokenResponseDto
        {
            Token = token,
            UserData = userDto
        }, "Login successful.");
    }

    // Updates an existing user's profile
    public async Task<ServiceResult> UpdateUserAsync(Guid id, UserRequestDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return ServiceResult.CreateFailure("User not found.");

        // Update fields
        user.Name = dto.Name;
        user.Designation = dto.Designation;
        user.Phone = dto.Phone;
        user.IsActive = dto.IsActive;

        await _userRepository.UpdateAsync(user);
        return ServiceResult.CreateSuccess("User updated.");
    }

    // Gets user details by ID
    public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : new UserResponseDto
        {
            Id = user.Id,
            SerialNumber = user.SerialNumber,
            Name = user.Name,
            Email = user.Email,
            Designation = user.Designation,
            Phone = user.Phone,
            IsActive = user.IsActive
        };
    }

    // Gets paginated and searchable list of users
    public async Task<PaginatedResult<UserResponseDto>> GetUsersAsync(string search, int pageNo, int rowsPerPage)
    {
        var users = await _userRepository.GetUsersAsync(search, pageNo, rowsPerPage);
        var total = await _userRepository.GetTotalCountAsync(search);

        // Calculate the starting serial number for the current page
        int startSerial = (pageNo - 1) * rowsPerPage + 1;

        var result = users.Select((u, index) => new UserResponseDto
        {
            Id = u.Id,
            SerialNumber = startSerial + index,  // assign sequential S.No dynamically
            Name = u.Name,
            Designation = u.Designation,
            Email = u.Email,
            Phone = u.Phone,
            IsActive = u.IsActive
        }).ToList();

        return PaginatedResult<UserResponseDto>.CreateSuccess(result, pageNo, rowsPerPage, total);
    }


    // Deletes user by ID
    public async Task<ServiceResult> DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return ServiceResult.CreateFailure("User not found.");

        await _userRepository.DeleteAsync(user);
        return ServiceResult.CreateSuccess("User deleted.");
    }

    // Updates user's password and clears IsFirst flag
    public async Task<ServiceResult> UpdatePasswordAsync(UpdatePasswordDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null) return ServiceResult.CreateFailure("User not found.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        user.IsFirst = false;
        await _userRepository.UpdateAsync(user);

        return ServiceResult.CreateSuccess("Password updated.");
    }

    // Sends OTP to user's email for password reset
    public async Task<ServiceResult> ForgotPasswordAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
            return ServiceResult.CreateFailure("User not found.");

        // Generate OTP and set expiry (use UTC)
        var otp = new Random().Next(100000, 999999).ToString();
        var otpExpiry = DateTime.UtcNow.AddMinutes(10);

        user.Otp = otp;
        user.OtpExpiry = otpExpiry;
        await _userRepository.UpdateAsync(user);

        // Load SMTP configuration
        var smtpServer = _configuration["EmailSettings:SmtpServer"];
        var smtpPort = int.Parse(_configuration["EmailSettings:Port"]);
        var fromEmail = _configuration["EmailSettings:Email"];
        var fromPassword = _configuration["EmailSettings:Password"];

        // HTML content (Gmail friendly)
        var htmlContent = $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset=""UTF-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <style>
    body {{
      font-family: Arial, sans-serif;
      background-color: #f4f4f4;
      padding: 0;
      margin: 0;
    }}
    .container {{
      max-width: 600px;
      margin: 40px auto;
      background-color: #ffffff;
      padding: 20px;
      border-radius: 8px;
      box-shadow: 0 0 10px rgba(0,0,0,0.05);
    }}
    .header {{
      text-align: center;
      padding-bottom: 20px;
    }}
    .otp-box {{
      font-size: 24px;
      font-weight: bold;
      background-color: #f0f0f0;
      padding: 15px;
      text-align: center;
      border-radius: 5px;
      letter-spacing: 2px;
    }}
    .footer {{
      font-size: 12px;
      color: #888888;
      text-align: center;
      padding-top: 20px;
    }}
  </style>
</head>
<body>
  <div class=""container"">
    <div class=""header"">
      <h2>Password Reset Request</h2>
    </div>
    <p>Hi {user.Name},</p>
    <p>You recently requested to reset your password. Please use the following OTP to proceed:</p>
    <div class=""otp-box"">{otp}</div>
    <p>This OTP is valid for 10 minutes.</p>
    <p>If you didn’t request this, please ignore this email.</p>
    <div class=""footer"">
      <p>&copy; {DateTime.UtcNow.Year} Endless Enterprise. All rights reserved.</p>
    </div>
  </div>
</body>
</html>";

        // Create email
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Endless Enterprise", fromEmail));
        message.To.Add(new MailboxAddress(user.Name, user.Email));
        message.Subject = "Reset Your Password";
        message.Body = new TextPart("html")
        {
            Text = htmlContent
        };

        // Send email via SMTP
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(fromEmail, fromPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        return ServiceResult.CreateSuccess("OTP sent successfully");
    }



    // Sends email using SMTP configuration
    public async Task SendEmailAsync(string toEmail, string toName, string subject, string htmlBody)
    {
        try
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPortString = _configuration["EmailSettings:Port"];
            var fromEmail = _configuration["EmailSettings:Email"];
            var fromPassword = _configuration["EmailSettings:Password"];

            if (string.IsNullOrEmpty(smtpPortString))
                throw new InvalidOperationException("SMTP port configuration is missing or invalid.");

            var smtpPort = int.Parse(smtpPortString);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Endless Enterprise", fromEmail));
            message.To.Add(new MailboxAddress(toName, toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = htmlBody };

            using var client = new SmtpClient();
            await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(fromEmail, fromPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Email sending error: {ex.Message}");
            throw;
        }
    }

    // Verifies OTP for password reset
    public async Task<ServiceResult> VerifyOtpAsync(string email, string otp)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null || user.Otp != otp)
            return ServiceResult.CreateFailure("Invalid or expired OTP");

        // Check OTP expiry using UTC time
        if (DateTime.UtcNow > user.OtpExpiry)
            return ServiceResult.CreateFailure("OTP expired");

        return ServiceResult.CreateSuccess("OTP verified successfully");
    }


    // Generates a random temporary password
    private string GenerateTemporaryPassword(int length = 10)
    {
        const string chars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#$!";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[new Random().Next(s.Length)]).ToArray());
    }

    // Placeholder JWT generator (replace with real JWT logic)
    private string GenerateToken(User user)
    {
        return "jwt-token-placeholder";
    }

    // Unused method stub
    // public Task<ServiceResult> SendOtpAsync(string email)
    // {
    //     throw new NotImplementedException();
    // }
}
