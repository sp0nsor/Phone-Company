namespace PhoneCompany.Infrastructure.Entities
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid? PhoneNumberId { get; set; }
        public PhoneNumberEntity? PhoneNumber { get; set; }
    }
}
