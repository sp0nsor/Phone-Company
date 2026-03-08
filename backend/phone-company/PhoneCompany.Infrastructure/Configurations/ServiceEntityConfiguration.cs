using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneCompany.Infrastructure.Entities;

namespace PhoneCompany.Infrastructure.Configurations
{
    public class ServiceEntityConfiguration
        : IEntityTypeConfiguration<ServiceEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Price)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasAnnotation("CheckConstraint", "Price >= 0");

            builder.HasMany(s => s.TariffPlans)
                .WithMany(t => t.Services)
                .UsingEntity<TariffPlanServiceEntity>(
                    l => l.HasOne<TariffPlanEntity>().WithMany().HasForeignKey(tsp => tsp.TariffPlanId),
                    r => r.HasOne<ServiceEntity>().WithMany().HasForeignKey(tsp => tsp.ServiceId));

        }
    }
}
