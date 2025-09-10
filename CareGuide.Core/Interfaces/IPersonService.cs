using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.Person;

namespace CareGuide.Core.Interfaces
{
    public interface IPersonService
    {
        Task<List<PersonDto>> GetAllAsync(int page, int pageSize);
        Task<PersonDto> GetAsync(Guid id);
        Task<PersonDto> CreateAsync(CreatePersonDto createPerson);
        Task<PersonDto> UpdateAsync(Guid id, PersonDto updatePerson);
        Task DeleteAsync(Guid id);
    }
}
