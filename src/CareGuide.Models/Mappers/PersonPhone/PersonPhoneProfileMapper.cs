using AutoMapper;
using CareGuide.Models.DTOs.PersonPhone;

namespace CareGuide.Models.Mappers.PersonPhone
{
    public class PersonPhoneProfileMapper : Profile
    {
        public PersonPhoneProfileMapper()
        {
            CreateMap<Entities.PersonPhone, PersonPhoneDto>()
                .ForCtorParam(nameof(PersonPhoneDto.PersonId), opt => opt.MapFrom(src => src.PersonId))
                .ForCtorParam(nameof(PersonPhoneDto.PhoneId), opt => opt.MapFrom(src => GetPhone(src).Id))
                .ForCtorParam(nameof(PersonPhoneDto.Number), opt => opt.MapFrom(src => GetPhone(src).Number))
                .ForCtorParam(nameof(PersonPhoneDto.AreaCode), opt => opt.MapFrom(src => GetPhone(src).AreaCode))
                .ForCtorParam(nameof(PersonPhoneDto.Type), opt => opt.MapFrom(src => GetPhone(src).Type));
        }

        private static Entities.Phone GetPhone(Entities.PersonPhone source)
        {
            return source.Phone
                ?? throw new InvalidOperationException("Phone not found.");
        }
    }
}