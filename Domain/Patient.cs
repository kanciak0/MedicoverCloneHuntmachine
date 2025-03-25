using System.Collections.Generic;
using System;

namespace MedicoverClone.Domain
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ContactInfo { get; set; }
        public string Address { get; set; }
        public List<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
