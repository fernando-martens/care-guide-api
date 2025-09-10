using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.Person;

namespace CareGuide.Core.Interfaces
{
    public interface IPersonService
    {
        List<PersonDto> GetAll(int page = PaginationConstants.DefaultPage, int pageSize = PaginationConstants.DefaultPageSize);
        PersonDto Get(Guid id);
        PersonDto Create(CreatePersonDto person);
        PersonDto Update(Guid id, PersonDto person);
        void Delete(Guid id);
    }
}
