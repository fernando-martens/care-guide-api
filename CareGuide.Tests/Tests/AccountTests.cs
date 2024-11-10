using CareGuide.Models.DTOs.Auth;

namespace CareGuide.Tests.Tests
{
    public class AccountTests: Base
    {
        public AccountTests(Program program): base(program) 
        {
           
        }

        [Fact(DisplayName = "Should be able to create an account")]
        internal void Test1()
        {
            Func<string> generateFakeEmail = () => $"{Guid.NewGuid()}@emailtest.com";

            CreateAccountDto createAccount = new CreateAccountDto()
            {
                Email = generateFakeEmail(),
                Password = "Test123",
                Name = "Test name",
                Gender = Models.Enums.Gender.F,
                Birthday = DateOnly.FromDateTime(DateTime.Now)
            };

            AccountDto account = _accountService.CreateAccount(createAccount);

            Assert.Equal(createAccount.Name, account.Name);
            Assert.Equal(createAccount.Email, account.Email);
            Assert.Equal(createAccount.Gender, account.Gender);
            Assert.Equal(createAccount.Birthday, account.Birthday);

            Assert.Equal("Email already registered", Assert.Throws<Exception>(() =>
            {
                _accountService.CreateAccount(createAccount);
            }).Message);

            createAccount.Email = generateFakeEmail();
            _accountService.CreateAccount(createAccount);

        }
    }
}
