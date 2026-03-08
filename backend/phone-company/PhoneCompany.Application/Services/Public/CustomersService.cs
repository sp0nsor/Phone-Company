using AutoMapper;
using PhoneCompany.Application.DTOs;
using PhoneCompany.Application.Interfaces.Public;
using PhoneCompany.Application.Requests;
using PhoneCompany.Application.Requests.Customer;
using PhoneCompany.Infrastructure.Entities;
using PhoneCompany.Infrastructure.Enums;
using PhoneCompany.Infrastructure.Interfaces;
using PhoneCompany.Infrastructure.Specifications.Customer;

namespace PhoneCompany.Application.Services.Public
{
    public class CustomersService : ICustomersService
    {
        private readonly IRepository<CustomerEntity> _customerRepository;
        private readonly IPhoneNumbersService _phoneNumbersService;
        private readonly IMapper _mapper;

        public CustomersService(
            IMapper mapper,
            IRepository<CustomerEntity> customerRepository,
            IPhoneNumbersService phoneNumbersService)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _phoneNumbersService = phoneNumbersService;
        }

        public async Task<List<CustomerDto>> GetCustomersAsync(
            PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            var specification = new GetCustomersSpecification();
            var (customerEntities, totalPages) = await _customerRepository.GetAsync(
                specification,
                paginationParams.PageIndex,
                paginationParams.PageSize, 
                cancellationToken);

            return _mapper.Map<List<CustomerDto>>(customerEntities);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(
            Guid customerId,
            CancellationToken cancellationToken)
        {
            var specification = new GetCustomerByIdSpecification(customerId);
            var customerEntity = await _customerRepository.GetSingleAsync(specification, cancellationToken);

            return _mapper.Map<CustomerDto>(customerEntity);
        }

        public async Task CreateCustomerAsync(
            CreateCustomerRequest createCustomerRequest,
            CancellationToken cancellationToken)
        {
            var freePhoneNumber = await _phoneNumbersService
                .GetFreePhoneNumberAsync(cancellationToken);

            var customerEntity = new CustomerEntity
            {
                Id = Guid.NewGuid(),
                Name = createCustomerRequest.Name,
                Email = createCustomerRequest.Email,
                PhoneNumberId = freePhoneNumber.Id,
            };

            await _customerRepository.CreateAsync(
                customerEntity,
                cancellationToken);

            await _phoneNumbersService.ChangePhoneNumberStatusAsync(
                freePhoneNumber.Id,
                PhoneStatus.Active,
                cancellationToken);
        }

        public async Task DeleteCustomerAsync(
            Guid customerId, 
            CancellationToken cancellationToken)
        {
            var specification = new GetCustomerByIdSpecification(customerId);
            var customerEntity = await _customerRepository.GetSingleAsync(specification, cancellationToken);

            if (customerEntity is null)
                throw new Exception("Customer not foud");

            Console.WriteLine(customerEntity.PhoneNumberId.Value);

            await _phoneNumbersService.ChangePhoneNumberStatusAsync(
                customerEntity.PhoneNumberId.Value,
                PhoneStatus.Inactive,
                cancellationToken);

            await _customerRepository.DeleteAsync(customerEntity, cancellationToken);
        }

        public async Task UpdateCustomerAsync(
            Guid customerId,
            UpdateCustomerRequest updateCustomerRequest,
            CancellationToken cancellationToken)
        {
            var specification = new GetCustomerByIdSpecification(customerId);
            var customerEntity = await _customerRepository.GetSingleAsync(specification, cancellationToken);

            customerEntity.Name = updateCustomerRequest.Name;
            customerEntity.Email = updateCustomerRequest.Email;

            await _customerRepository.UpdateAsync(customerEntity, cancellationToken);
        }
    }
}
