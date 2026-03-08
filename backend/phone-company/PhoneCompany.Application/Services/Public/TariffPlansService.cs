using AutoMapper;
using PhoneCompany.Application.DTOs;
using PhoneCompany.Application.Interfaces.Public;
using PhoneCompany.Application.Requests;
using PhoneCompany.Application.Requests.TariffPlan;
using PhoneCompany.Infrastructure.Entities;
using PhoneCompany.Infrastructure.Interfaces;
using PhoneCompany.Infrastructure.Specifications.TariffPlan;

namespace PhoneCompany.Application.Services.Public
{
    public class TariffPlansService : ITariffPlansService
    {
        private readonly IMapper _mapper;
        private readonly IServicesService _servicesService;
        private readonly IRepository<TariffPlanEntity> _tariffPlanRepository;

        public TariffPlansService(
            IRepository<TariffPlanEntity> tariffPlanRepository,
            IMapper mapper,
            IServicesService servicesService)
        {
            _tariffPlanRepository = tariffPlanRepository;
            _mapper = mapper;
            _servicesService = servicesService;
        }

        public async Task<PaginatedResponse<TariffPlanDto>> GetTariffPlansAsync(
            PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            var specification = new GetTariffPlansSpecification();
            var (tariffPlanEntities, totalPages) = await _tariffPlanRepository.GetAsync(
                specification,
                paginationParams.PageIndex,
                paginationParams.PageSize,
                cancellationToken);

            return new()
            {
                Items = _mapper.Map<List<TariffPlanDto>>(tariffPlanEntities),
                PageCount = totalPages,
                PageIndex = paginationParams.PageIndex,
                PageSize = paginationParams.PageSize
            };
        }

        public async Task<TariffPlanDto> GetTariffPlanByIdAsync(
            Guid tariffPlanId,
            CancellationToken cancellationToken)
        {
            var specification = new GetTariffPlanByIdSpecification(tariffPlanId);
            var tariffPlanEntity = await _tariffPlanRepository.GetSingleAsync(
                specification,
                cancellationToken);

            if (tariffPlanEntity is null)
                throw new Exception("Tariff plan not found");

            return _mapper.Map<TariffPlanDto>(tariffPlanEntity);
        }

        public async Task DeleteTariffPlanAsync(
            Guid tariffPlanId,
            CancellationToken cancellationToken)
        {
            var specification = new GetTariffPlanByIdSpecification(tariffPlanId);
            var tariffPlanEntity = await _tariffPlanRepository.GetSingleAsync(
                specification,
                cancellationToken);

            await _tariffPlanRepository.DeleteAsync(
                tariffPlanEntity,
                cancellationToken);
        }

        public async Task CreateTariffPlanAsync(
            CreateTariffPlanRequest createTariffPlanRequest,
            CancellationToken cancellationToken)
        {
            var tariffPlanEntity = new TariffPlanEntity
            {
                Id = Guid.NewGuid(),
                Name = createTariffPlanRequest.Name,
                Description = createTariffPlanRequest.Description,
                Price = 0
            };

            await _tariffPlanRepository.CreateAsync(
                tariffPlanEntity,
                cancellationToken);
        }

        public async Task UpdateTariffPlanAsync(
            Guid tariffPlanId,
            UpdateTariffPlanRequest updateTariffPlanRequest,
            CancellationToken cancellationToken)
        {
            var specification = new GetTariffPlanByIdSpecification(tariffPlanId);
            var tariffPlanEntity = await _tariffPlanRepository.GetSingleAsync(
                specification,
                cancellationToken);

            tariffPlanEntity.Name = updateTariffPlanRequest.Name;
            tariffPlanEntity.Description = updateTariffPlanRequest.Description;

            await _tariffPlanRepository.UpdateAsync(
                tariffPlanEntity,
                cancellationToken);
        }

        public async Task AddServiceToTariffPlanAsync(
            Guid tariffPlanId,
            Guid serviceId,
            CancellationToken cancellationToken)
        {
            var specification = new GetTariffPlanByIdSpecification(tariffPlanId);
            var tariffPlanEntity = await _tariffPlanRepository.GetSingleAsync(
                specification,
                cancellationToken);

            if (tariffPlanEntity is null)
                throw new Exception("Tariff plan not found");

            var service = await _servicesService.GetServiceByIdAsync(
                serviceId,
                cancellationToken);

            var serviceEntity = _mapper.Map<ServiceEntity>(service);

            tariffPlanEntity.Services.Add(serviceEntity);

            tariffPlanEntity.Price = tariffPlanEntity.Services
                .Sum(s => s.Price);

            await _tariffPlanRepository.UpdateAsync(
                tariffPlanEntity,
                cancellationToken);
        }
    }
}
