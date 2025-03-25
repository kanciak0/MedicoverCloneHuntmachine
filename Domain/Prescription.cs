using System;

namespace MedicoverClone.Domain
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public DateTime Date { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
        public Patient Patient { get; set; }
        public Doctor PrescribingDoctor { get; set; }
    }

}
