using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PhoneCompany.Application.Requests.Service
{
    public record UpdateServiceRequest(
        string Name,
        float Price);
}
