namespace PhoneCompany.Application.DTOs
{
    public record CustomerDto(
        Guid Id,
        string Name,
        string Email,
        PhoneNumberDto? PhoneNumber);
}
