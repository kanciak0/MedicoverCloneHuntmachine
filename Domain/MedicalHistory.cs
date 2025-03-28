using System;

namespace MedicoverClone.Domain
{
    public class MedicalHistory
    {
        public int HistoryId { get; set; }
        public DateTime DateOfVisit { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public Patient Patient { get; set; }
        public string Notes { get; set; }
    }

}
