// File: Tiles.Core/ServiceContracts/UserManagement/Application/Interfaces/IUserService.cs

using System;
using System.Threading.Tasks;
using Tiles.Core.DTO; // Necessary because used DTOs are inside this namespace
using Tiles.Core.Wrappers;

namespace Tiles.Core.ServiceContracts.UserManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult> RegisterUserAsync(UserRequestDto dto);
        Task<PaginatedResult<UserResponseDto>> GetUsersAsync(string search, int pageNo, int rowsPerPage);
        Task<UserResponseDto?> GetUserByIdAsync(Guid id);
        Task<ServiceResult> UpdateUserAsync(Guid id, UserRequestDto dto);
        Task<ServiceResult> DeleteUserAsync(Guid id);
        Task<ServiceResult<TokenResponseDto>> LoginAsync(LoginDto dto);
        Task<ServiceResult> UpdatePasswordAsync(UpdatePasswordDto dto);
        Task<ServiceResult> SendOtpAsync(string email);
        Task<ServiceResult> VerifyOtpAsync(string email, string otp);
        Task SendEmailAsync(string to, string subject, string body,bool isHtml);
        Task<ServiceResult> ForgotPasswordAsync(string email);
    }
}
