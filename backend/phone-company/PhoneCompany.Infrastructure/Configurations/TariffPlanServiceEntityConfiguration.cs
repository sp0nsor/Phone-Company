using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneCompany.Infrastructure.Entities;

namespace PhoneCompany.Infrastructure.Configurations
{
    public class TariffPlanServiceEntityConfiguration
        : IEntityTypeConfiguration<TariffPlanServiceEntity>
    {
        public void Configure(EntityTypeBuilder<TariffPlanServiceEntity> builder)
        {
            builder.HasKey(tps => new { tps.TariffPlanId, tps.ServiceId });
        }
    }
}
