using CareGuide.Models.DTOs.PersonAnnotation;

namespace CareGuide.Core.Interfaces
{
    public interface IPersonAnnotationService
    {
        List<PersonAnnotationDto> GetAllByPerson(Guid personId);
        PersonAnnotationDto GetById(Guid id);
        PersonAnnotationDto Create(CreatePersonAnnotationDto personAnnotation);
        PersonAnnotationDto Update(Guid id, UpdatePersonAnnotationDto personAnnotation);
        void DeleteAllByPerson(Guid personId);
        void DeleteByIds(List<Guid> ids);
    }
}
