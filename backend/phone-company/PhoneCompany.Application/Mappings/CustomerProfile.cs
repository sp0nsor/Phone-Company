using AutoMapper;
using PhoneCompany.Application.DTOs;
using PhoneCompany.Infrastructure.Entities;

namespace PhoneCompany.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerEntity, CustomerDto>();
        }
    }
}
