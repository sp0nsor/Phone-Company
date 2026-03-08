using AutoMapper;
using PhoneCompany.Application.DTOs;
using PhoneCompany.Infrastructure.Entities;

namespace PhoneCompany.Application.Mappings
{
    public class TariffPlanProfile : Profile
    {
        public TariffPlanProfile()
        {
            CreateMap<TariffPlanEntity, TariffPlanDto>();
        }
    }
}
