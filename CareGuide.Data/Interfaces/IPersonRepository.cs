using CareGuide.Models.Tables;


namespace CareGuide.Data.Interfaces
{
    public interface IPersonRepository
    {
        List<PersonTable> ListAll();
        PersonTable ListById(Guid id);
        PersonTable Insert(PersonTable person);
        PersonTable Update(PersonTable person);
        void Remove(PersonTable person);
    }
}
