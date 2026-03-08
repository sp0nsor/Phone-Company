using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneCompany.Infrastructure.Entities;

namespace PhoneCompany.Infrastructure.Configurations
{
    public class TariffPlanEntityConfiguration
        : IEntityTypeConfiguration<TariffPlanEntity>
    {
        public void Configure(EntityTypeBuilder<TariffPlanEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(t => t.Price)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasAnnotation("CheckConstraint", "Price >= 0");

            builder.HasMany(t => t.Numbers)
                .WithOne(p => p.TariffPlan)
                .HasForeignKey(p => p.TariffPalnId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
