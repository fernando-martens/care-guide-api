using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Repositories
{
    public class PersonRepository(DatabaseContext context) : BaseRepository<Person>(context), IPersonRepository
    {

    }
}
