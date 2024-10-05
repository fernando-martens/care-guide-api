using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.Tables;

namespace CareGuide.Core.Services
{
    public class PersonService : IPersonService
    {

        private readonly IUserService _userService;
        private readonly IPersonRepository _personRepository;

        public PersonService(IUserService userService, IPersonRepository personRepository)
        {
            _userService = userService;
            _personRepository = personRepository;
        }

        public List<Person> ListAll()
        {
            return _personRepository.ListAll();
        }

        public Person ListById(Guid id)
        {
            return _personRepository.ListById(id);
        }

        public Person Insert(PersonRequestDto person)
        {
            _userService.ListById(person.UserId);

            Person personToCreate = new Person
            {
                Id = Guid.NewGuid(),
                UserId = person.UserId,
                Name = person.Name,
                Gender = person.Gender,
                Birthday = person.Birthday,
                Register = DateTime.UtcNow
            };

            return _personRepository.Insert(personToCreate);
        }

        public Person Update(Guid id, PersonRequestDto person)
        {
            Person existingPerson = _personRepository.ListById(id);
            existingPerson.UserId = person.UserId;
            existingPerson.Name = person.Name;
            existingPerson.Gender = person.Gender;
            existingPerson.Birthday = person.Birthday;
            existingPerson.Register = DateTime.UtcNow;

            return _personRepository.Update(existingPerson);
        }

        public void Remove(Guid id)
        {
            Person existingPerson = _personRepository.ListById(id);
            _personRepository.Remove(existingPerson);
        }
    }

}
