﻿namespace CareGuide.Security
{
    public class SecuritySettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}