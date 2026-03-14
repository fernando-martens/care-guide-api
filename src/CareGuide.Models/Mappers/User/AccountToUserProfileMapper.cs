using AutoMapper;
using CareGuide.Models.DTOs.Account;
using CareGuide.Models.DTOs.User;

namespace CareGuide.Models.Mappers.User
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
