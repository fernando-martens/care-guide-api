using CareGuide.Models.Enums;

namespace CareGuide.Models.Tables
{
    public class Person
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public DateTime Register { get; set; }

        public User? User { get; set; }
    }
}
