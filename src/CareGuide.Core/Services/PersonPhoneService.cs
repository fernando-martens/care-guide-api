using AutoMapper;
using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Data.TransactionManagement;
using CareGuide.Models.DTOs.PersonPhone;
using CareGuide.Models.DTOs.Phone;
using CareGuide.Models.Entities;

namespace CareGuide.Core.Services
{
    public class PersonPhoneService : IPersonPhoneService
    {
        private readonly IPersonPhoneRepository _personPhoneRepository;
        private readonly IPhoneService _phoneService;
        private readonly IEfTransactionUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonPhoneService(IPersonPhoneRepository personPhoneRepository, IPhoneService phoneService, IEfTransactionUnitOfWork unitOfWork, IMapper mapper)
        {
            _personPhoneRepository = personPhoneRepository;
            _phoneService = phoneService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PersonPhoneDto> GetAllByPersonAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var personPhones = await _personPhoneRepository.GetAllByPersonWithPhonesAsync(page, pageSize, cancellationToken);

            if (personPhones.Count == 0)
                throw new KeyNotFoundException("No phone records found for the logged-in person.");

            var personId = personPhones.First().PersonId ?? throw new InvalidOperationException("PersonId not found.");
            var phones = _mapper.Map<ICollection<PhoneDto>>(personPhones.Select(pp => pp.Phone));

            return new PersonPhoneDto(personId, phones);
        }

        public async Task<PersonPhoneDto> GetAsync(Guid phoneId, CancellationToken cancellationToken)
        {
            var personPhone = await _personPhoneRepository.GetByPersonWithPhoneAsync(phoneId, cancellationToken);

            if (personPhone == null)
                throw new UnauthorizedAccessException("You are not authorized to access this phone record or it does not exist.");

            var personId = personPhone.PersonId ?? throw new InvalidOperationException("PersonId not found.");
            var phoneDto = _mapper.Map<PhoneDto>(personPhone.Phone);

            return new PersonPhoneDto(personId, new List<PhoneDto> { phoneDto });
        }

        public async Task<PersonPhoneDto> CreateAsync(CreatePhoneDto phoneDto, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var createdPhone = await _phoneService.CreateAsync(phoneDto, cancellationToken);

                var personPhone = new PersonPhone
                {
                    PhoneId = createdPhone.Id
                };

                await _personPhoneRepository.AddAsync(personPhone, cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                var personId = personPhone.PersonId ?? throw new InvalidOperationException("PersonId not found.");
                var phoneResult = _mapper.Map<PhoneDto>(createdPhone);

                return new PersonPhoneDto(personId, new List<PhoneDto> { phoneResult });
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }

        }

        public async Task<PersonPhoneDto> UpdateAsync(Guid id, UpdatePhoneDto phoneDto, CancellationToken cancellationToken)
        {
            if (phoneDto == null)
                throw new ArgumentNullException(nameof(phoneDto));

            var personPhone = await _personPhoneRepository.GetByPersonWithPhoneAsync(phoneDto.Id, cancellationToken);

            if (personPhone == null)
                throw new UnauthorizedAccessException("You are not authorized to update this phone record or it does not exist.");

            var updatedPhone = await _phoneService.UpdateAsync(phoneDto.Id, phoneDto, cancellationToken);

            var personId = personPhone.PersonId ?? throw new InvalidOperationException("PersonId not found.");
            var phoneResult = _mapper.Map<PhoneDto>(updatedPhone);

            return new PersonPhoneDto(personId, new List<PhoneDto> { phoneResult });
        }

        public async Task DeleteAllByPersonAsync(CancellationToken cancellationToken)
        {
            var personPhones = await _personPhoneRepository.GetAllByPersonWithPhonesAsync(1, int.MaxValue, cancellationToken);

            if (personPhones == null || personPhones.Count == 0)
                return;

            var phoneIds = personPhones.Select(pp => pp.PhoneId).ToList();

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await _personPhoneRepository.DeleteAllByPersonAsync(cancellationToken);
                await _phoneService.DeleteByIdsAsync(phoneIds, cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task DeleteByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            if (ids == null || ids.Count == 0)
                throw new ArgumentException("The list of IDs cannot be empty.", nameof(ids));

            var personPhones = await _personPhoneRepository.GetManyByPersonAndIdsAsync(ids, cancellationToken);

            if (personPhones == null || personPhones.Count == 0)
                throw new UnauthorizedAccessException("No valid phone records found for this person.");

            var phoneIds = personPhones.Select(pp => pp.PhoneId).ToList();

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await _personPhoneRepository.DeleteManyAsync(personPhones.Select(pp => pp.Id), cancellationToken);
                await _phoneService.DeleteByIdsAsync(phoneIds, cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
