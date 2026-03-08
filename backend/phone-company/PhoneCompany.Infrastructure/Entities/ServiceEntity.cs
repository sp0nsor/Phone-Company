namespace PhoneCompany.Infrastructure.Entities
{
    public class ServiceEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public ICollection<TariffPlanEntity> TariffPlans { get; set; } = [];
    }
}
