using AutoMapper;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.Person;

namespace CareGuide.Models.Mappers
{
    public class AccountToPersonProfileMapper : Profile
    {
        public AccountToPersonProfileMapper()
        {
            CreateMap<CreateAccountDto, CreatePersonDto>()
                .ConstructUsing(src => new CreatePersonDto(
                    Guid.Empty,
                    src.Name,
                    src.Gender,
                    src.Birthday,
                    null
                ));
        }
    }
}
