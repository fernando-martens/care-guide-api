﻿using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Person
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public Gender Gender { get; set; }
        public DateOnly Birthday { get; set; }
    }
}
