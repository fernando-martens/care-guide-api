using AutoMapper;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.User;

namespace CareGuide.Models.Mappers
{
    public class AccountToUserProfileMapper : Profile
    {
        public AccountToUserProfileMapper()
        {
            CreateMap<CreateAccountDto, CreateUserDto>()
                .ConstructUsing(src => new CreateUserDto(
                    Guid.Empty,
                    Guid.Empty,
                    src.Email,
                    src.Password
                ));
        }
    }
}
