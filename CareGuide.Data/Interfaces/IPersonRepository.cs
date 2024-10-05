using CareGuide.Models.Tables;


namespace CareGuide.Data.Interfaces
{
    public interface IPersonRepository
    {
        List<Person> ListAll();
        Person ListById(Guid id);
        Person Insert(Person person);
        Person Update(Person person);
        void Remove(Person person);
    }
}
