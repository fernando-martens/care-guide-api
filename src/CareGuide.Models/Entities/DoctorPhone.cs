namespace CareGuide.Models.Entities
{
    public class DoctorPhone
    {
        public Guid DoctorId { get; set; }
        public Guid PhoneId { get; set; }

        public Doctor Doctor { get; set; } = null!;
        public Phone Phone { get; set; } = null!;
    }
}
