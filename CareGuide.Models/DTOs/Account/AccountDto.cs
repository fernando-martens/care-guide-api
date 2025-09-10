using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Auth
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string SessionToken { get; set; }
        public required string Name { get; set; }
        public Gender Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public string? Picture { get; set; }
    }
}
