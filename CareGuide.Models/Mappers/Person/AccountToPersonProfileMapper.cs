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
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Picture, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
