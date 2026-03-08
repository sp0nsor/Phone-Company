using AutoMapper;
using PhoneCompany.Application.DTOs;
using PhoneCompany.Infrastructure.Entities;

namespace PhoneCompany.Application.Mappings
{
    public class PhoneNumberProfile : Profile
    {
        public PhoneNumberProfile()
        {
            CreateMap<PhoneNumberEntity, PhoneNumberDto>();
        }
    }
}
