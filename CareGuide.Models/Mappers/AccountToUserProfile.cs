using AutoMapper;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.User;

namespace CareGuide.Models.Mappers
{
  public class AccountToUserProfile : Profile
  {
    public AccountToUserProfile()
    {
      CreateMap<CreateAccountDto, CreateUserDto>()
          .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
          .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
          .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
  }
}
