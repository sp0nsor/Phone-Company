using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneCompany.Infrastructure.Entities;
using PhoneCompany.Infrastructure.Enums;

namespace PhoneCompany.Infrastructure.Configurations
{
    public class RoleEntityConfiguration
        : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(5);

            var roles = Enum
                .GetValues<Role>()
                .Select(role => new RoleEntity
                {
                    Id = (int)role,
                    Name = role.ToString()
                });

            builder.HasData(roles);
        }
    }
}
