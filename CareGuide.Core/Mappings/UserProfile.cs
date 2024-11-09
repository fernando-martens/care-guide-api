using AutoMapper;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.Tables;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<PersonTable, PersonDto>();
    }
}
