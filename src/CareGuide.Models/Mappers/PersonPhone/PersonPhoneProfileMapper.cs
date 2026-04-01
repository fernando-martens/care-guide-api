using AutoMapper;
using CareGuide.Models.DTOs.PersonPhone;

namespace CareGuide.Models.Mappers.PersonPhone
{
    public class PersonPhoneProfileMapper : Profile
    {
        public PersonPhoneProfileMapper()
        {
            CreateMap<Entities.PersonPhone, PersonPhoneDto>()
                .ForCtorParam(nameof(PersonPhoneDto.PersonId), opt => opt.MapFrom(src => GetPersonId(src)))
                .ForCtorParam(nameof(PersonPhoneDto.Id), opt => opt.MapFrom(src => GetPhone(src).Id))
                .ForCtorParam(nameof(PersonPhoneDto.Number), opt => opt.MapFrom(src => GetPhone(src).Number))
                .ForCtorParam(nameof(PersonPhoneDto.AreaCode), opt => opt.MapFrom(src => GetPhone(src).AreaCode))
                .ForCtorParam(nameof(PersonPhoneDto.Type), opt => opt.MapFrom(src => GetPhone(src).Type))
                .ForCtorParam(nameof(PersonPhoneDto.CreatedAt), opt => opt.MapFrom(src => GetPhone(src).CreatedAt))
                .ForCtorParam(nameof(PersonPhoneDto.UpdatedAt), opt => opt.MapFrom(src => GetPhone(src).UpdatedAt));
        }

        private static Guid GetPersonId(Entities.PersonPhone source)
        {
            return source.PersonId
                ?? throw new InvalidOperationException("PersonId not found.");
        }

        private static Entities.Phone GetPhone(Entities.PersonPhone source)
        {
            return source.Phone
                ?? throw new InvalidOperationException("Phone not found.");
        }
    }
}