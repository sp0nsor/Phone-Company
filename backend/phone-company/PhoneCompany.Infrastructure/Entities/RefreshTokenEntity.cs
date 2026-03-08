namespace PhoneCompany.Infrastructure.Entities
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public UserEntity? User { get; set; } 
    }
}
