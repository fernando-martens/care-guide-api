using CareGuide.Models.Entities.Shared;

namespace CareGuide.Models.Entities
{
    public class DoctorSpecialty : Entity
    {
        public required Guid DoctorId { get; set; }
        public required string Name { get; set; }

        public Doctor Doctor { get; set; } = null!;
    }
}
