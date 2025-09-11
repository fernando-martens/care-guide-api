using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Data.TransactionManagement;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.DTOs.User;
using CareGuide.Models.Tables;
using CareGuide.Security;
using CareGuide.Security.Interfaces;

namespace CareGuide.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IPersonService _personService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly AutoMapper.IMapper _mapper;

        public AccountService(IUserService userService, IUserRepository userRepository, IPersonService personService, IUnitOfWork unitOfWork, IJwtService jwtService, AutoMapper.IMapper mapper)
        {
            _userService = userService;
            _userRepository = userRepository;
            _personService = personService;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<AccountDto> CreateAccountAsync(CreateAccountDto createAccount, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var createPersonDto = _mapper.Map<CreatePersonDto>(createAccount);
                var createUserDto = _mapper.Map<CreateUserDto>(createAccount);

                PersonDto person = await _personService.CreateAsync(createPersonDto, cancellationToken);
                createUserDto.PersonId = person.Id;

                UserDto user = await _userService.CreateAsync(person, createUserDto, cancellationToken);

                string token = _jwtService.GenerateToken(user.Id, user.Email);

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return new AccountDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    SessionToken = token,
                    Name = person.Name,
                    Gender = person.Gender,
                    Birthday = person.Birthday,
                    Picture = person.Picture
                };
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<AccountDto> LoginAccountAsync(LoginAccountDto loginAccount, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByEmailAsync(loginAccount.Email, cancellationToken);

            if (user == null || !PasswordManager.ValidatePassword(loginAccount.Password, user.Password))
                throw new InvalidOperationException("Wrong password or email");

            string token = _jwtService.GenerateToken(user.Id, loginAccount.Email);

            UserDto userDto = await _userService.GetByIdDtoAsync(user.Id, cancellationToken);
            PersonDto personDto = await _personService.GetAsync(userDto.PersonId, cancellationToken);

            return new AccountDto
            {
                Id = userDto.Id,
                Email = userDto.Email,
                SessionToken = token,
                Name = personDto.Name,
                Gender = personDto.Gender,
                Birthday = personDto.Birthday,
                Picture = personDto.Picture
            };
        }

        public async Task UpdatePasswordAccountAsync(Guid id, UpdatePasswordAccountDto updatePasswordAccount, CancellationToken cancellationToken)
        {
            await _userService.UpdatePasswordAsync(id, updatePasswordAccount, cancellationToken);
        }

        public async Task DeleteAccountAsync(Guid id, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var user = await _userService.GetByIdAsync(id, cancellationToken);

                if (user == null)
                    throw new KeyNotFoundException($"User with id {id} not found");

                await _userService.DeleteAsync(id, cancellationToken);

                await _personService.DeleteAsync(user.PersonId, cancellationToken);

                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
