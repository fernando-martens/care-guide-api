using CareGuide.Models.DTOs.PersonAnnotation;

namespace CareGuide.Core.Interfaces
{
    public interface IPersonAnnotationService
    {
        Task<List<PersonAnnotationDto>> GetAllByPersonAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<PersonAnnotationDto> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<PersonAnnotationDto> CreateAsync(CreatePersonAnnotationDto personAnnotation, CancellationToken cancellationToken);
        Task<PersonAnnotationDto> UpdateAsync(Guid id, UpdatePersonAnnotationDto personAnnotation, CancellationToken cancellationToken);
        Task DeleteAllByPersonAsync(CancellationToken cancellationToken);
        Task DeleteByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    }
}
