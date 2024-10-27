using CareGuide.Models.DTOs.Person;

namespace CareGuide.Core.Interfaces
{
    public interface IPersonService
    {
        List<PersonDto> ListAll();
        PersonDto Select(Guid id);
        PersonDto Create(CreatePersonDto person);
        PersonDto Update(Guid id, PersonDto person);
        void Delete(Guid id);
    }
}
