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

        public AccountService(IUserService userService, IUserRepository userRepository, IPersonService personService, IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _userService = userService;
            _userRepository = userRepository;
            _personService = personService;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<AccountDto> CreateAccountAsync(CreateAccountDto createAccount)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                PersonDto person = await _personService.CreateAsync(new CreatePersonDto(createAccount));
                UserDto user = await _userService.CreateAsync(person, new CreateUserDto(createAccount));

                string token = _jwtService.GenerateToken(user.Id, user.Email);

                await _unitOfWork.CommitTransactionAsync();

                return new AccountDto(user, person, token);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<AccountDto> LoginAccountAsync(LoginAccountDto loginAccount)
        {
            User? user = await _userRepository.GetByEmailAsync(loginAccount.Email);

            if (user == null || !PasswordManager.ValidatePassword(loginAccount.Password, user.Password))
                throw new InvalidOperationException("wrong password or email");

            string token = _jwtService.GenerateToken(user.Id, loginAccount.Email);

            UserDto userDto = await _userService.GetByIdDtoAsync(user.Id);
            PersonDto personDto = await _personService.GetAsync(userDto.PersonId);

            return new AccountDto(userDto, personDto, token);
        }

        public async Task UpdatePasswordAccountAsync(Guid id, UpdatePasswordAccountDto updatePasswordAccount)
        {
            await _userService.UpdatePasswordAsync(id, updatePasswordAccount);
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
