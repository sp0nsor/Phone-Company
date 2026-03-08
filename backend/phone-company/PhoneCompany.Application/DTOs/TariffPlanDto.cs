namespace PhoneCompany.Application.DTOs
{
    public record TariffPlanDto(
        Guid Id,
        string Name,
        string Description,
        float Price,
        ICollection<ServiceDto> Services);
}
