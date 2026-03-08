using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PhoneCompany.Application.DTOs;
using PhoneCompany.Application.Interfaces.Internal;
using PhoneCompany.Application.Interfaces.Public;
using PhoneCompany.Application.Requests.User;
using PhoneCompany.Infrastructure.Entities;
using PhoneCompany.Infrastructure.Interfaces;
using PhoneCompany.Infrastructure.Specifications.User;

namespace PhoneCompany.Application.Services.Public
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IMapper _mapper;

        public AuthService(
            IMapper mapper,
            IRefreshTokenService refreshTokenService,
            IAccessTokenService accessTokenService,
            IPasswordService passwordService,
            IRepository<UserEntity> userRepository)
        {
            _mapper = mapper;
            _refreshTokenService = refreshTokenService;
            _accessTokenService = accessTokenService;
            _passwordService = passwordService;
            _userRepository = userRepository;
        }

        public async Task RegisterUserAsync(
            RegisterUserRequest registerUserRequest,
            CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<UserEntity>(registerUserRequest);

            entity.PasswordHash = _passwordService
                .Generate(registerUserRequest.Password);

            await _userRepository.CreateAsync(
                entity,
                cancellationToken);
        }

        public async Task<LoginDto> LoginUserAsync(
            LoginUserRequest loginUserRequest,
            CancellationToken cancellationToken)
        {
            var specification = new GetUserByEmailSpecification(loginUserRequest.Email);
            var user = await _userRepository.GetSingleAsync(
                specification,
                cancellationToken) 
                ?? throw new UnauthorizedAccessException("Invalid credentials");

            if (!_passwordService.Verify(loginUserRequest.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            if (user.RefreshToken != null)
            {
                await _refreshTokenService.DeleteRefreshTokenAsync(
                    user.RefreshToken, 
                    cancellationToken);
            }

            var accessToken = _accessTokenService.GenerateAccessToken(user);
            var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(cancellationToken);

            user.RefreshToken = refreshToken;
            await _userRepository.UpdateAsync(user, cancellationToken);

            return new LoginDto(accessToken, refreshToken.Value);
        }

        public async Task<LoginDto> RefreshUserTokens(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var specification = new GetUserByIdSpecification(userId);
            var user = await _userRepository.GetSingleAsync(
                specification,
                cancellationToken);

/*            if(user.RefreshToken.Expires <= DateTime.UtcNow)
                throw new Exception("You need to re-login");*/

            var newAccessToken = _accessTokenService.GenerateAccessToken(user);

            var newRefreshToken = await _refreshTokenService
                .CreateRefreshTokenAsync(cancellationToken);

            await _refreshTokenService.DeleteRefreshTokenAsync(
                user.RefreshToken,
                cancellationToken);

            user.RefreshToken = newRefreshToken;

            await _userRepository.UpdateAsync(
                user,
                cancellationToken);

            return new LoginDto(newAccessToken, newRefreshToken.Value);
        }
    }


}
