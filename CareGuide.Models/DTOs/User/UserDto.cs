﻿namespace CareGuide.Models.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Email { get; set; }
        public string SessionToken { get; set; }
        public DateTime Register { get; set; }
    }
}
