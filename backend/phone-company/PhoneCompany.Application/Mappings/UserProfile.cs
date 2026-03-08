using AutoMapper;
using PhoneCompany.Application.Requests.User;
using PhoneCompany.Infrastructure.Entities;
using PhoneCompany.Infrastructure.Enums;

namespace PhoneCompany.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserRequest, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => (int)Role.Admin));
        }
    }
}
