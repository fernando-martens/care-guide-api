using AutoMapper;
using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.Exceptions;
using CareGuide.Models.Tables;

namespace CareGuide.Core.Services
{
    public class PersonService : IPersonService
    {

        private readonly IUserService _userService;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IUserService userService, IPersonRepository personRepository, IMapper mapper)
        {
            _userService = userService;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public List<PersonDto> ListAll()
        {
            List<PersonTable> list = _personRepository.ListAll();
            return _mapper.Map<List<PersonDto>>(list);
        }

        public PersonDto Select(Guid id)
        {
            PersonTable person =_personRepository.SelectById(id) ?? throw new NotFoundException();
            return _mapper.Map<PersonDto>(person);
        }

        public PersonDto Create(CreatePersonDto createPerson)
        {
            PersonTable person = _personRepository.Insert(new PersonTable(createPerson));
            return _mapper.Map<PersonDto>(person);
        }

        public PersonDto Update(Guid id, PersonDto updatePerson)
        {
            PersonTable existingPerson = _personRepository.SelectById(id) ?? throw new NotFoundException();

            existingPerson.Name = updatePerson.Name;
            existingPerson.Gender = updatePerson.Gender;
            existingPerson.Birthday = updatePerson.Birthday;
            existingPerson.Picture = updatePerson.Picture;

            PersonTable person = _personRepository.Update(existingPerson);

            return _mapper.Map<PersonDto>(person);
        }

        public void Delete(Guid id)
        {
            PersonTable existingPerson = _personRepository.SelectById(id) ?? throw new NotFoundException();
            _personRepository.Remove(existingPerson);
        }

    }

}
