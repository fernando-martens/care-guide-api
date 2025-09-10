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

        public async Task<List<PersonAnnotationDto>> GetAllByPersonAsync(Guid personId)
        {
            if (personId == Guid.Empty)
                throw new ArgumentException("The personId cannot be empty.", nameof(personId));

            var list = await _personAnnotationRepository.GetAllByPersonAsync(personId);
            return _mapper.Map<List<PersonAnnotationDto>>(list);
        }

        public async Task<PersonAnnotationDto> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("The id cannot be empty.", nameof(id));

            var personAnnotationTable = await _personAnnotationRepository.GetAsync(id);

            if (personAnnotationTable == null)
                throw new KeyNotFoundException($"No person annotation found with the ID {id}.");

            return _mapper.Map<PersonAnnotationDto>(personAnnotationTable);
        }

        public async Task<PersonAnnotationDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonAnnotationDto> CreateAsync(CreatePersonAnnotationDto personAnnotation)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonAnnotationDto> UpdateAsync(Guid id, UpdatePersonAnnotationDto personAnnotation)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAllByPersonAsync(Guid personId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByIdsAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }
    }
}
