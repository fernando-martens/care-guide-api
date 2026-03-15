namespace CareGuide.Security.DTOs
{
    public class SecuritySettingsDto
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}