using CareGuide.Models.Tables;

namespace CareGuide.Data.Interfaces
{
    public interface IPersonAnnotationRepository
    {
        List<PersonAnnotationTable> ListAllByPerson(Guid personId);
        PersonAnnotationTable? SelectById(Guid id);
        PersonTable Insert(PersonTable person);
        PersonTable Update(PersonTable person);
        void RemoveAllByPerson(Guid personId);
        void Remove(Guid id);
    }
}
