namespace CareGuide.Security.Interfaces
{
    public interface IUserSessionContext
    {
        Guid UserId { get; }
        Guid PersonId { get; }
        string Email { get; }
    }
}
