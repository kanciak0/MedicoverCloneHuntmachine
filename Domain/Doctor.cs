using System.Collections.Generic;

namespace MedicoverClone.Domain
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public string ContactInfo { get; set; }
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
