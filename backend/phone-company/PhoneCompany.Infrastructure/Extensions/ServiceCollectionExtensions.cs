using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneCompany.Infrastructure.Entities;
using PhoneCompany.Infrastructure.Interfaces;
using PhoneCompany.Infrastructure.Repositories;

namespace PhoneCompany.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IRepository<UserEntity>, Repository<UserEntity>>();
            services.AddScoped<IRepository<RefreshTokenEntity>, Repository<RefreshTokenEntity>>();

            services.AddScoped<IRepository<CustomerEntity>, Repository<CustomerEntity>>();
            services.AddScoped<IRepository<PhoneNumberEntity>, Repository<PhoneNumberEntity>>();
            services.AddScoped<IRepository<ServiceEntity>, Repository<ServiceEntity>>();
            services.AddScoped<IRepository<TariffPlanEntity>, Repository<TariffPlanEntity>>();

            services.AddDbContext<PhoneCompanyDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(PhoneCompanyDbContext)));
            });

            return services;
        }
    }
}
