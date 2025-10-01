﻿using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Data.TransactionManagement;
using CareGuide.Models.DTOs.Account;
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
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly AutoMapper.IMapper _mapper;

        public AccountService(IUserService userService, IUserRepository userRepository, IPersonService personService, IUnitOfWork unitOfWork, IJwtService jwtService, IRefreshTokenService refreshTokenService, AutoMapper.IMapper mapper)
        {
            _userService = userService;
            _userRepository = userRepository;
            _personService = personService;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
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
                var createUserDtoWithPerson = createUserDto with { PersonId = person.Id };

                UserDto user = await _userService.CreateAsync(person, createUserDtoWithPerson, cancellationToken);

                var accessToken = _jwtService.GenerateToken(user.Id, person.Id, user.Email);
                var refreshToken = await _refreshTokenService.CreateAsync(user.Id, cancellationToken);

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return new AccountDto(
                    user.Id,
                    user.Email,
                    accessToken,
                    refreshToken.Token,
                    person.Name,
                    person.Gender,
                    person.Birthday
                );
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

            var accessToken = _jwtService.GenerateToken(user.Id, user.PersonId, loginAccount.Email);
            var refreshToken = await _refreshTokenService.CreateAsync(user.Id, cancellationToken);

            var userDto = await _userService.GetByIdDtoAsync(user.Id, cancellationToken);
            var personDto = await _personService.GetAsync(userDto.PersonId, cancellationToken);

            return new AccountDto(
                userDto.Id,
                userDto.Email,
                accessToken,
                refreshToken.Token,
                personDto.Name,
                personDto.Gender,
                personDto.Birthday
            );
        }

        public async Task<AccountDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(refreshTokenDto.Email, cancellationToken);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid email.");

            var newRefresh = await _refreshTokenService.RotateAsync(user.Id, refreshTokenDto.RefreshToken, cancellationToken);
            string newAccessToken = _jwtService.GenerateToken(user.Id, user.PersonId, user.Email);

            var userDto = await _userService.GetByIdDtoAsync(user.Id, cancellationToken);
            var personDto = await _personService.GetAsync(user.PersonId, cancellationToken);

            return new AccountDto(
                userDto.Id,
                userDto.Email,
                newAccessToken,
                newRefresh.Token,
                personDto.Name,
                personDto.Gender,
                personDto.Birthday
            );
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
