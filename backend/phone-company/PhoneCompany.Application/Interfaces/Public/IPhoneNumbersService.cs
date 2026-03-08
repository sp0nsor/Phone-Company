using PhoneCompany.Application.DTOs;
using PhoneCompany.Application.Requests;
using PhoneCompany.Application.Requests.PhoneNumber;
using PhoneCompany.Infrastructure.Enums;

namespace PhoneCompany.Application.Interfaces.Public
{
    public interface IPhoneNumbersService
    {
        Task AddTariffPlanToNumberAsync(Guid phoneNumberId, Guid tariffPlanId, CancellationToken cancellationToken);
        Task ChangePhoneNumberStatusAsync(Guid phoneNumberId, PhoneStatus status, CancellationToken cancellationToken);
        Task CreatePhoneNumberAsync(CancellationToken cancellationToken);
        Task DeletePhoneNumberAsync(Guid phoneNumberId, CancellationToken cancellationToken);
        Task<PhoneNumberDto> GetFreePhoneNumberAsync(CancellationToken cancellationToken);
        Task<PhoneNumberDto> GetPhoneNumberByIdAsync(Guid phoneNumberId, CancellationToken cancellationToken);
        Task<List<PhoneNumberDto>> GetPhoneNumbersAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
        Task UpdatePhoneNumberAsync(Guid phoneNumberId, UpdatePhoneNumberRequest updatePhoneNumberRequest, CancellationToken cancellationToken);
    }
}