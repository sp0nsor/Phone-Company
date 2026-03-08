using PhoneCompany.Infrastructure.Entities;

namespace PhoneCompany.Application.Interfaces.Internal
{
    public interface IAccessTokenService
    {
        string GenerateAccessToken(UserEntity user);
    }
}