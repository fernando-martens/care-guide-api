using Zxcvbn;
namespace CareGuide.Security
{
    public static class PasswordManager
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        public static bool ValidatePassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public static (bool IsSecure, string Feedback) CheckPassword(string password)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(password))
                errors.Add("Password cannot be empty.");
            if (!password.Any(char.IsUpper))
                errors.Add("Password must contain at least one uppercase letter.");
            if (!password.Any(char.IsLower))
                errors.Add("Password must contain at least one lowercase letter.");
            if (!password.Any(char.IsDigit))
                errors.Add("Password must contain at least one number.");
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                errors.Add("Password must contain at least one special character.");

            if (errors.Count > 0)
                return (false, string.Join(" ", errors));

            var result = Core.EvaluatePassword(password);
            if (result.Score >= 3)
                return (true, string.Empty);

            var suggestions = result.Feedback.Suggestions != null ? string.Join(" ", result.Feedback.Suggestions) : string.Empty;
            var warning = result.Feedback.Warning ?? string.Empty;
            var message = $"{warning} {suggestions}".Trim();

            return (false, string.IsNullOrWhiteSpace(message) ? "Password is too weak." : message);
        }
    }
}
