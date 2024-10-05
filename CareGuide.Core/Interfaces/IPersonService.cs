using CareGuide.Models.DTOs.Person;
using CareGuide.Models.Tables;

namespace CareGuide.Core.Interfaces
{
    public interface IPersonService
    {
        List<Person> ListAll();
        Person ListById(Guid id);
        Person Insert(PersonRequestDto person);
        Person Update(Guid id, PersonRequestDto person);
        void Remove(Guid id);
    }
}
