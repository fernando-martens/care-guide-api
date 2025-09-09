using AutoMapper;
using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Models.DTOs.PersonAnnotation;
using CareGuide.Models.Tables;

namespace CareGuide.Core.Services
{
    public class PersonAnnotationService : IPersonAnnotationService
    {

        private readonly IPersonAnnotationRepository _personAnnotationRepository;
        private readonly IMapper _mapper;

        public PersonAnnotationService(IPersonAnnotationRepository personAnnotationRepository, IMapper mapper)
        {
            _personAnnotationRepository = personAnnotationRepository;
            _mapper = mapper;
        }

        public List<PersonAnnotationDto> SelectAllByPerson(Guid personId)
        {
            if (personId == Guid.Empty)
                throw new ArgumentException("The personId cannot be empty.", nameof(personId));

            List<PersonAnnotationTable> list = _personAnnotationRepository.ListAllByPerson(personId);
            return _mapper.Map<List<PersonAnnotationDto>>(list);
        }

        public PersonAnnotationDto SelectById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("The id cannot be empty.", nameof(id));

            var personAnnotationTable = _personAnnotationRepository.SelectById(id)
                ?? throw new KeyNotFoundException($"No person annotation found with the ID {id}.");

            return _mapper.Map<PersonAnnotationDto>(personAnnotationTable);
        }

        public PersonAnnotationDto Select(Guid id)
        {
            throw new NotImplementedException();
        }

        public PersonAnnotationDto Create(CreatePersonAnnotationDto personAnnotation)
        {
            throw new NotImplementedException();
        }

        public PersonAnnotationDto Update(Guid id, UpdatePersonAnnotationDto personAnnotation)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllByPerson(Guid personId)
        {
            throw new NotImplementedException();
        }

        public void DeleteByIds(List<Guid> ids)
        {
            throw new NotImplementedException();
        }
    }
}
