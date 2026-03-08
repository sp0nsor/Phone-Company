namespace PhoneCompany.Application.DTOs
{
    public record LoginDto(
        string AccessToken,
        string RefreshToken);
}
