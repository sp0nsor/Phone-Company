using PhoneCompany.Application.DTOs;
using PhoneCompany.Application.Requests;
using PhoneCompany.Application.Requests.Customer;

namespace PhoneCompany.Application.Interfaces.Public
{
    public interface ICustomersService
    {
        Task CreateCustomerAsync(CreateCustomerRequest createCustomerRequest, CancellationToken cancellationToken);
        Task<CustomerDto> GetCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken);
        Task DeleteCustomerAsync(Guid customerId, CancellationToken cancellationToken);
        Task UpdateCustomerAsync(Guid customerId, UpdateCustomerRequest updateCustomerRequest, CancellationToken cancellationToken);
        Task<List<CustomerDto>> GetCustomersAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
    }
}