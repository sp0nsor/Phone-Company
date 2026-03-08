using PhoneCompany.Infrastructure.Entities;

namespace PhoneCompany.Application.Interfaces.Internal
{
    public interface IRefreshTokenService
    {
        Task<RefreshTokenEntity> CreateRefreshTokenAsync(CancellationToken cancellationToken);
        Task DeleteRefreshTokenAsync(RefreshTokenEntity refreshToken, CancellationToken cancellationToken);
        Task<RefreshTokenEntity> GetRefreshTokenAsync(Guid id, CancellationToken cancellationToken);
    }
}