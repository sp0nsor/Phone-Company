using AutoMapper;
using PhoneCompany.Application.DTOs;
using PhoneCompany.Infrastructure.Entities;

namespace PhoneCompany.Application.Mappings
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<ServiceEntity, ServiceDto>();

            CreateMap<ServiceDto, ServiceEntity>();
        }
    }
}
