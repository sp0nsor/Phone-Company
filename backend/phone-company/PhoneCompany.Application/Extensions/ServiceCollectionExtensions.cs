using Microsoft.Extensions.DependencyInjection;
using PhoneCompany.Application.Interfaces.Internal;
using PhoneCompany.Application.Interfaces.Public;
using PhoneCompany.Application.Mappings;
using PhoneCompany.Application.Services.Internal;
using PhoneCompany.Application.Services.Public;

namespace PhoneCompany.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CustomerProfile));
            services.AddAutoMapper(typeof(TariffPlanProfile));
            services.AddAutoMapper(typeof(PhoneNumberProfile));
            services.AddAutoMapper(typeof(ServiceProfile));

            services.AddScoped<ICustomersService, CustomersService>();
            services.AddScoped<ITariffPlansService, TariffPlansService>();
            services.AddScoped<IServicesService, ServicesService>();
            services.AddScoped<IPhoneNumbersService, PhoneNumbersService>();

            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IAccessTokenService, AccessTokenService>();

            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
