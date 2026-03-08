using PhoneCompany.Application.DTOs;
using PhoneCompany.Application.Requests;
using PhoneCompany.Application.Requests.Service;

namespace PhoneCompany.Application.Interfaces.Public
{
    public interface IServicesService
    {
        Task CreateServiceAsync(CreateServiceRequest createServiceRequest, CancellationToken cancellationToken);
        Task DeleteServiceAsync(Guid serviceId, CancellationToken cancellationToken);
        Task<ServiceDto> GetServiceByIdAsync(Guid serviceId, CancellationToken cancellationToken);
        Task<List<ServiceDto>> GetServicesAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
        Task UpdateServiceAsync(Guid serviceId, UpdateServiceRequest updateServiceRequest, CancellationToken cancellationToken);
    }
}