using Microsoft.Extensions.Options;
using PhoneCompany.Application.Interfaces.Internal;
using PhoneCompany.Application.Options;
using PhoneCompany.Infrastructure.Entities;
using PhoneCompany.Infrastructure.Interfaces;
using PhoneCompany.Infrastructure.Specifications.RefreshToken;
using System.Security.Cryptography;

namespace PhoneCompany.Application.Services.Internal
{
    internal class RefreshTokenService : IRefreshTokenService
    {
        private readonly RefreshTokenOptions _options;
        private readonly IRepository<RefreshTokenEntity> _repository;
        public RefreshTokenService(IOptions<RefreshTokenOptions> options,
            IRepository<RefreshTokenEntity> repository)
        {
            _options = options.Value;
            _repository = repository;
        }

        public async Task<RefreshTokenEntity> GetRefreshTokenAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var specification = new GetRefreshTokenByIdSpecification(id);
            var entity = await _repository.GetSingleAsync(
                specification,
                cancellationToken);

            if (entity is null)
                throw new Exception("You need to re-login");

            return entity;
        }

        public async Task<RefreshTokenEntity> CreateRefreshTokenAsync(
            CancellationToken cancellationToken)
        {
            var value = GenerateValue();

            var entity = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                Value = value,
                Expires = DateTime.UtcNow.AddDays(_options.ExpiresDays)
            };

            await _repository.CreateAsync(
                entity,
                cancellationToken);

            return entity;
        }

        public async Task DeleteRefreshTokenAsync(
            RefreshTokenEntity refreshToken,
            CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(
                refreshToken,
                cancellationToken);
        }

        private string GenerateValue()
        {
            var randomBytes = new byte[_options.TokenLength];
            RandomNumberGenerator.Fill(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }
    }
}
