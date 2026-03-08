using PhoneCompany.Application.DTOs;
using PhoneCompany.Application.Requests;
using PhoneCompany.Application.Requests.TariffPlan;

namespace PhoneCompany.Application.Interfaces.Public
{
    public interface ITariffPlansService
    {
        Task<PaginatedResponse<TariffPlanDto>> GetTariffPlansAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
        Task<TariffPlanDto> GetTariffPlanByIdAsync(Guid tariffPlanId, CancellationToken cancellationToken);
        Task AddServiceToTariffPlanAsync(Guid tariffPlanId, Guid serviceId, CancellationToken cancellationToken);
        Task CreateTariffPlanAsync(CreateTariffPlanRequest createTariffPlanRequest, CancellationToken cancellationToken);
        Task DeleteTariffPlanAsync(Guid tariffPlanId, CancellationToken cancellationToken);
        Task UpdateTariffPlanAsync(Guid tariffPlanId, UpdateTariffPlanRequest updateTariffPlanRequest, CancellationToken cancellationToken);
    }
}