﻿namespace CareGuide.Security.Interfaces
{
    public interface IUserSessionContext
    {
        Guid? UserId { get; }
    }
}
