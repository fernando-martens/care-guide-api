using AutoMapper;
using CareGuide.Models.DTOs.User;

namespace CareGuide.Models.Mappers.User
{
    public class UserProfileMapper : Profile
    {
        public UserProfileMapper()
        {
            CreateMap<Entities.User, UserDto>();
            CreateMap<CreateUserDto, Entities.User>();
        }
    }
}
