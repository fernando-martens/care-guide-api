using AutoMapper;
using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.Tables;

namespace CareGuide.Core.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public List<PersonDto> GetAll(int page = PaginationConstants.DefaultPage, int pageSize = PaginationConstants.DefaultPageSize)
        {
            List<Person> list = _personRepository.GetAll(page, pageSize);
            return _mapper.Map<List<PersonDto>>(list);
        }

        public PersonDto Select(Guid id)
        {
            Person person = _personRepository.Get(id) ?? throw new KeyNotFoundException();
            return _mapper.Map<PersonDto>(person);
        }

        public PersonDto Create(CreatePersonDto createPerson)
        {
            Person person = _mapper.Map<Person>(createPerson);
            _personRepository.Add(person);
            return _mapper.Map<PersonDto>(person);
        }

        public PersonDto Update(Guid id, PersonDto updatePerson)
        {
            Person existingPerson = _personRepository.Get(id) ?? throw new KeyNotFoundException();

            existingPerson.Name = updatePerson.Name;
            existingPerson.Gender = updatePerson.Gender;
            existingPerson.Birthday = updatePerson.Birthday;
            existingPerson.Picture = updatePerson.Picture;

            Person person = _personRepository.Update(existingPerson);

            return _mapper.Map<PersonDto>(person);
        }

        public void Delete(Guid id)
        {
            Person existingPerson = _personRepository.Get(id) ?? throw new KeyNotFoundException();
            _personRepository.Delete(existingPerson.Id);
        }

    }

}
