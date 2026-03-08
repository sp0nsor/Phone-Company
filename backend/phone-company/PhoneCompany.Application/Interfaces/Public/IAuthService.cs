using PhoneCompany.Application.DTOs;
using PhoneCompany.Application.Requests.User;

namespace PhoneCompany.Application.Interfaces.Public
{
    public interface IAuthService
    {
        Task<LoginDto> LoginUserAsync(LoginUserRequest loginUserRequest, CancellationToken cancellationToken);
        Task<LoginDto> RefreshUserTokens(Guid userId, CancellationToken cancellationToken);
        Task RegisterUserAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken);
    }
}