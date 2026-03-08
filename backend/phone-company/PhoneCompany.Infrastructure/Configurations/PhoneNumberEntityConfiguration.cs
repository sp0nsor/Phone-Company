using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneCompany.Infrastructure.Entities;
using PhoneCompany.Infrastructure.Enums;

namespace PhoneCompany.Infrastructure.Configurations
{
    public class PhoneNumberEntityConfiguration
        : IEntityTypeConfiguration<PhoneNumberEntity>
    {
        public void Configure(EntityTypeBuilder<PhoneNumberEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(13);

            builder.Property(p => p.Status)
                .IsRequired()
                .HasDefaultValue(PhoneStatus.Inactive)
                .HasConversion<string>();

            builder.HasOne(p => p.Customer)
                .WithOne(c => c.PhoneNumber)
                .HasForeignKey<PhoneNumberEntity>(p => p.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
