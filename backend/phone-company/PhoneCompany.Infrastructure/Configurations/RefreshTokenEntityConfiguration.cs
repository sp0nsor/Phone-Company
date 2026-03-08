using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneCompany.Infrastructure.Entities;

namespace PhoneCompany.Infrastructure.Configurations
{
    public class RefreshTokenEntityConfiguration
        : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Value)
                .IsRequired();

            builder.Property(t => t.Expires)
                .IsRequired()
                .HasConversion(
                    e => e.ToUniversalTime(),
                    e => DateTime.SpecifyKind(e, DateTimeKind.Utc));
        }
    }
}
