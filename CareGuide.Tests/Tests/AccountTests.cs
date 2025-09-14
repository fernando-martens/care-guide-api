using CareGuide.Models.DTOs.Auth;

namespace CareGuide.Tests.Tests
{
    public class AccountTests : Base
    {
        public AccountTests(Program program) : base(program)
        {

        }

        [Fact(DisplayName = "Should be able to create an account")]
        public async Task Test1()
        {
            Func<string> generateFakeEmail = () => $"{Guid.NewGuid()}@emailtest.com";

            CreateAccountDto createAccount = new CreateAccountDto(
                generateFakeEmail(),
                "Test123",
                "Test name",
                Models.Enums.Gender.F,
                DateOnly.FromDateTime(DateTime.Now)
            );

            AccountDto account = await _accountService.CreateAccountAsync(createAccount, CancellationToken.None);

            Assert.Equal(createAccount.Name, account.Name);
            Assert.Equal(createAccount.Email, account.Email);
            Assert.Equal(createAccount.Gender, account.Gender);
            Assert.Equal(createAccount.Birthday, account.Birthday);

            var ex = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _accountService.CreateAccountAsync(createAccount, CancellationToken.None);
            });
            Assert.Equal("Email already registered", ex.Message);

            var createAccount2 = createAccount with { Email = generateFakeEmail() };
            await _accountService.CreateAccountAsync(createAccount2, CancellationToken.None);
        }
    }
}
