using Microsoft.EntityFrameworkCore;

namespace PhoneCompany.Infrastructure
{
    public class PhoneCompanyDbContext : DbContext
    {
        public PhoneCompanyDbContext(DbContextOptions<PhoneCompanyDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhoneCompanyDbContext).Assembly);
        }
    }
}
