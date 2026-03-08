using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneCompany.Infrastructure.Entities;

namespace PhoneCompany.Infrastructure.Configurations
{
    public class CustomerEntityConfiguration
        : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(c => c.PhoneNumber)
                .WithOne(p => p.Customer)
                .HasForeignKey<CustomerEntity>(c => c.PhoneNumberId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
