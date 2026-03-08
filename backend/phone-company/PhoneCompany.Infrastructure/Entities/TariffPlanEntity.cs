namespace PhoneCompany.Infrastructure.Entities
{
    public class TariffPlanEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public ICollection<PhoneNumberEntity> Numbers { get; set; } = [];
        public ICollection<ServiceEntity> Services { get; set; } = [];
    }
}
