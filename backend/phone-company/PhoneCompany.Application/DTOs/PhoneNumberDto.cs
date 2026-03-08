using PhoneCompany.Infrastructure.Enums;

namespace PhoneCompany.Application.DTOs
{
    public record PhoneNumberDto(
        Guid Id,
        string Value,
        PhoneStatus Status);
}
