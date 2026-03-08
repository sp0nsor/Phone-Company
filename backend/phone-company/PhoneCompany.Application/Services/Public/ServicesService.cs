using AutoMapper;
using PhoneCompany.Application.DTOs;
using PhoneCompany.Application.Interfaces.Public;
using PhoneCompany.Application.Requests;
using PhoneCompany.Application.Requests.Service;
using PhoneCompany.Infrastructure.Entities;
using PhoneCompany.Infrastructure.Interfaces;
using PhoneCompany.Infrastructure.Specifications.Service;

namespace PhoneCompany.Application.Services.Public
{
    public class ServicesService : IServicesService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ServiceEntity> _servicesRepository;

        public ServicesService(
            IMapper mapper,
            IRepository<ServiceEntity> servicesRepository)
        {
            _mapper = mapper;
            _servicesRepository = servicesRepository;
        }

        public async Task<List<ServiceDto>> GetServicesAsync(
            PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            var specification = new GetServicesSpecification();
            var (serviceEntities, totalPages) = await _servicesRepository.GetAsync(
                specification,
                paginationParams.PageIndex,
                paginationParams.PageSize,
                cancellationToken);

            return _mapper.Map<List<ServiceDto>>(serviceEntities);
        }

        public async Task<ServiceDto> GetServiceByIdAsync(
            Guid serviceId,
            CancellationToken cancellationToken)
        {
            var specification = new GetServiceByIdSpecification(serviceId);
            var serviceEntity = await _servicesRepository.GetSingleAsync(
                specification,
                cancellationToken);

            if (serviceEntity is null)
                throw new Exception("Service not found");

            return _mapper.Map<ServiceDto>(serviceEntity);
        }

        public async Task UpdateServiceAsync(
            Guid serviceId,
            UpdateServiceRequest updateServiceRequest,
            CancellationToken cancellationToken)
        {
            var specification = new GetServiceByIdSpecification(serviceId);
            var serviceEntity = await _servicesRepository.GetSingleAsync(
                specification,
                cancellationToken);

            serviceEntity.Price = updateServiceRequest.Price;
            serviceEntity.Name = updateServiceRequest.Name;

            await _servicesRepository.UpdateAsync(
                serviceEntity,
                cancellationToken);
        }

        public async Task CreateServiceAsync(
            CreateServiceRequest createServiceRequest,
            CancellationToken cancellationToken)
        {
            var serviceEntity = new ServiceEntity
            {
                Id = Guid.NewGuid(),
                Name = createServiceRequest.Name,
                Price = createServiceRequest.Price
            };

            await _servicesRepository.CreateAsync(
                serviceEntity,
                cancellationToken);
        }

        public async Task DeleteServiceAsync(
            Guid serviceId,
            CancellationToken cancellationToken)
        {
            var specification = new GetServiceByIdSpecification(serviceId);
            var serviceEntity = await _servicesRepository.GetSingleAsync(
                specification,
                cancellationToken);

            await _servicesRepository.DeleteAsync(
                serviceEntity,
                cancellationToken);
        }
    }
}
