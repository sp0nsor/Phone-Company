namespace PhoneCompany.Application.Requests.User
{
    public record RegisterUserRequest(
        string Username,
        string Password,
        string Email);
}
