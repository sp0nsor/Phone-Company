using PhoneCompany.Infrastructure.Enums;

namespace PhoneCompany.Infrastructure.Entities
{
    public class PhoneNumberEntity
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public PhoneStatus Status { get; set; }
        public Guid? CustomerId { get; set; }
        public CustomerEntity? Customer { get; set; }
        public Guid? TariffPalnId { get; set; }
        public TariffPlanEntity? TariffPlan { get; set; }
    }
}
