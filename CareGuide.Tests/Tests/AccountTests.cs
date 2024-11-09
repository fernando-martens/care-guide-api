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
            CreateAccountDto createAccount = new CreateAccountDto()
            {
                Email = $"{Guid.NewGuid()}@outlook.com",
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

        }
    }
}
