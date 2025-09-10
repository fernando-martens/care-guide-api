using CareGuide.Models.DTOs.PersonAnnotation;

namespace CareGuide.Core.Interfaces
{
    public interface IPersonAnnotationService
    {
        Task<List<PersonAnnotationDto>> GetAllByPersonAsync(Guid personId);
        Task<PersonAnnotationDto> GetAsync(Guid id);
        Task<PersonAnnotationDto> CreateAsync(CreatePersonAnnotationDto personAnnotation);
        Task<PersonAnnotationDto> UpdateAsync(Guid id, UpdatePersonAnnotationDto personAnnotation);
        Task DeleteAllByPersonAsync(Guid personId);
        Task DeleteByIdsAsync(List<Guid> ids);
    }
}
