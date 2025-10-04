using AutoMapper;
using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.Entities;

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

        public async Task<List<PersonDto>> GetAllAsync(int page = PaginationConstants.DefaultPage, int pageSize = PaginationConstants.DefaultPageSize, CancellationToken cancellationToken = default)
        {
            var list = await _personRepository.GetAllAsync(page, pageSize, cancellationToken);
            return _mapper.Map<List<PersonDto>>(list);
        }

        public async Task<PersonDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var person = await _personRepository.GetAsync(id, cancellationToken);

            if (person == null)
                throw new KeyNotFoundException();

            return _mapper.Map<PersonDto>(person);
        }

        public async Task<PersonDto> CreateAsync(CreatePersonDto createPerson, CancellationToken cancellationToken = default)
        {
            var person = _mapper.Map<Person>(createPerson);
            await _personRepository.AddAsync(person, cancellationToken);
            return _mapper.Map<PersonDto>(person);
        }

        public async Task<PersonDto> UpdateAsync(Guid id, PersonDto updatePerson, CancellationToken cancellationToken = default)
        {
            var existingPerson = await _personRepository.GetAsync(id, cancellationToken);

            if (existingPerson == null)
                throw new KeyNotFoundException();

            existingPerson.Name = updatePerson.Name;
            existingPerson.Gender = updatePerson.Gender;
            existingPerson.Birthday = updatePerson.Birthday;
            existingPerson.Picture = updatePerson.Picture;

            var person = await _personRepository.UpdateAsync(existingPerson, cancellationToken);
            return _mapper.Map<PersonDto>(person);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var existingPerson = await _personRepository.GetAsync(id, cancellationToken);

            if (existingPerson == null)
                throw new KeyNotFoundException();

            await _personRepository.DeleteAsync(existingPerson.Id, cancellationToken);
        }

    }
}
