using PhoneCompany.Application.Interfaces.Internal;

namespace PhoneCompany.Application.Services.Internal
{
    internal class PasswordService : IPasswordService
    {
        public string Generate(string password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verify(string password, string passwordHash) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
    }
}
