namespace CareGuide.Models.Tables
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? SessionToken { get; set; }
        public DateTime Register { get; set; }
    }
}
