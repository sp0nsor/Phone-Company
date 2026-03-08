namespace PhoneCompany.Application.Requests.User
{
    public record LoginUserRequest(
        string Email,
        string Password);
}
