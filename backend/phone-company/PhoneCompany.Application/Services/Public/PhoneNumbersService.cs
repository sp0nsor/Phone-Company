using AutoMapper;
using PhoneCompany.Application.DTOs;
using PhoneCompany.Application.Interfaces.Public;
using PhoneCompany.Application.Requests;
using PhoneCompany.Application.Requests.PhoneNumber;
using PhoneCompany.Infrastructure.Entities;
using PhoneCompany.Infrastructure.Enums;
using PhoneCompany.Infrastructure.Interfaces;
using PhoneCompany.Infrastructure.Specifications.PhoneNumber;

namespace PhoneCompany.Application.Services.Public
{
    public class PhoneNumbersService : IPhoneNumbersService
    {
        private readonly IMapper _mapper;
        private readonly ITariffPlansService _tariffPlanService;
        private readonly IRepository<PhoneNumberEntity> _phoneNumbersRepository;

        public PhoneNumbersService(
            ITariffPlansService tariffPlanService,
            IRepository<PhoneNumberEntity> phoneNumbersRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _tariffPlanService = tariffPlanService;
            _phoneNumbersRepository = phoneNumbersRepository;
        }

        public async Task<PhoneNumberDto> GetPhoneNumberByIdAsync(
            Guid phoneNumberId,
            CancellationToken cancellationToken)
        {
            var specification = new GetPhoneNumberByIdSpecification(phoneNumberId);
            var phoneNumberEntity = await _phoneNumbersRepository.GetSingleAsync(
                specification,
                cancellationToken);

            return _mapper.Map<PhoneNumberDto>(phoneNumberEntity);
        }

        public async Task<PhoneNumberDto> GetFreePhoneNumberAsync(
            CancellationToken cancellationToken)
        {
            var specification = new GetFreePhoneNumberSpecification();
            var phoneNumberEntity = await _phoneNumbersRepository.GetSingleAsync(
                specification,
                cancellationToken);

            if (phoneNumberEntity is null)
                throw new Exception("Free phone number doesn't exist");

            return _mapper.Map<PhoneNumberDto>(phoneNumberEntity);
        }

        public async Task<List<PhoneNumberDto>> GetPhoneNumbersAsync(
            PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            var specification = new GetPhoneNumbersSpecification();
            var (phoneNumberEntities, totalPages) = await _phoneNumbersRepository.GetAsync(
                specification,
                paginationParams.PageIndex,
                paginationParams.PageSize,
                cancellationToken);

            return _mapper.Map<List<PhoneNumberDto>>(phoneNumberEntities);
        }

        public async Task CreatePhoneNumberAsync(
            CancellationToken cancellationToken)
        {
            var phoneNumberEntity = new PhoneNumberEntity
            {
                Id = Guid.NewGuid(),
                Value = GeneratePhoneNumber(),
                Status = PhoneStatus.Inactive
            };

            await _phoneNumbersRepository.CreateAsync(
                phoneNumberEntity,
                cancellationToken);
        }

        public async Task UpdatePhoneNumberAsync(
            Guid phoneNumberId,
            UpdatePhoneNumberRequest updatePhoneNumberRequest,
            CancellationToken cancellationToken)
        {
            var specification = new GetPhoneNumberByIdSpecification(phoneNumberId);
            var phoneNumberEntity = await _phoneNumbersRepository.GetSingleAsync(
                specification,
                cancellationToken);

            phoneNumberEntity.Value = updatePhoneNumberRequest.Value;

            await _phoneNumbersRepository.UpdateAsync(
                phoneNumberEntity,
                cancellationToken);
        }

        public async Task DeletePhoneNumberAsync(
            Guid phoneNumberId,
            CancellationToken cancellationToken)
        {
            var specification = new GetPhoneNumberByIdSpecification(phoneNumberId);
            var phoneNumberEntity = await _phoneNumbersRepository.GetSingleAsync(
                specification,
                cancellationToken);

            await _phoneNumbersRepository.DeleteAsync(
                phoneNumberEntity,
                cancellationToken);
        }

        public async Task ChangePhoneNumberStatusAsync(
            Guid phoneNumberId,
            PhoneStatus status,
            CancellationToken cancellationToken)
        {
            var specification = new GetPhoneNumberByIdSpecification(phoneNumberId);
            var phoneNumberEntity = await _phoneNumbersRepository.GetSingleAsync(
                specification,
                cancellationToken);

            if (phoneNumberEntity is null)
                throw new Exception("Phone number doesn`t exist");

            phoneNumberEntity.Status = status;

            await _phoneNumbersRepository.UpdateAsync(
                phoneNumberEntity,
                cancellationToken);
        }

        public async Task AddTariffPlanToNumberAsync(
            Guid phoneNumberId,
            Guid tariffPlanId,
            CancellationToken cancellationToken)
        {
            var specification = new GetPhoneNumberByIdSpecification(phoneNumberId);
            var phoneNumberEntity = await _phoneNumbersRepository.GetSingleAsync(
                specification,
                cancellationToken);

            if (phoneNumberEntity is null)
                throw new Exception("Phone number not foud");

            var tariffPlan = await _tariffPlanService.GetTariffPlanByIdAsync(
                tariffPlanId,
                cancellationToken);

            phoneNumberEntity.TariffPalnId = tariffPlan.Id;

            await _phoneNumbersRepository.UpdateAsync(
                phoneNumberEntity, 
                cancellationToken);
        }

        private string GeneratePhoneNumber()
        {
            const string countryCode = "+375";
            var random = new Random();

            var digits = new char[9];
            for (int i = 0; i < digits.Length; i++)
            {
                digits[i] = (char)('0' + random.Next(0, 10));
            }

            return countryCode + new string(digits);
        }
    }
}
