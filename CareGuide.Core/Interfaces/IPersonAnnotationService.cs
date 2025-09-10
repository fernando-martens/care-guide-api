using CareGuide.Models.DTOs.PersonAnnotation;

namespace CareGuide.Core.Interfaces
{
    public interface IPersonAnnotationService
    {
        Task<List<PersonAnnotationDto>> GetAllByPersonAsync(Guid personId, CancellationToken cancellationToken);
        Task<PersonAnnotationDto> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<PersonAnnotationDto> CreateAsync(CreatePersonAnnotationDto personAnnotation, CancellationToken cancellationToken);
        Task<PersonAnnotationDto> UpdateAsync(Guid id, UpdatePersonAnnotationDto personAnnotation, CancellationToken cancellationToken);
        Task DeleteAllByPersonAsync(Guid personId, CancellationToken cancellationToken);
        Task DeleteByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    }
}
