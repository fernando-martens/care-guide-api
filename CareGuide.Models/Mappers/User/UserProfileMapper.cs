using AutoMapper;
using CareGuide.Models.DTOs.User;
using CareGuide.Models.Tables;

namespace CareGuide.Models.Mappers
{
    public class UserProfileMapper : Profile
    {
        public UserProfileMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
        }
    }
}
