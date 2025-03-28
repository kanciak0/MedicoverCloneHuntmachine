namespace MedicoverClone.ViewModels
{
    using MedicoverClone.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class HomePageViewModel
    {
        [Display(Name = "Total Patients")]
        public int TotalPatients { get; set; }

        [Display(Name = "Total Doctors")]
        public int TotalDoctors { get; set; }

        [Display(Name = "Today's Appointments")]
        public int AppointmentsToday { get; set; }

        [Display(Name = "Completed Today")]
        public int CompletedAppointmentsToday { get; set; }

        [Display(Name = "Pending Today")]
        public int PendingAppointmentsToday { get; set; }

        [Display(Name = "Upcoming Appointments")]
        public IEnumerable<Appointment> UpcomingAppointments { get; set; } = new List<Appointment>();

        [Display(Name = "Today's Schedule")]
        public IEnumerable<Appointment> TodaysAppointments { get; set; } = new List<Appointment>();

        [Display(Name = "Recent Medical Histories")]
        public IEnumerable<MedicalHistory> RecentMedicalHistories { get; set; } = new List<MedicalHistory>();

        [Display(Name = "Recent Prescriptions")]
        public IEnumerable<Prescription> RecentPrescriptions { get; set; } = new List<Prescription>();

        [Display(Name = "Appointment Summary")]
        public Dictionary<AppointmentStatus, int> AppointmentStatusSummary { get; set; }
            = Enum.GetValues(typeof(AppointmentStatus))
                .Cast<AppointmentStatus>()
                .ToDictionary(status => status, status => 0);

        [Display(Name = "Appointments by Specialty")]
        public Dictionary<string, int> AppointmentsBySpecialty { get; set; }
            = new Dictionary<string, int>();

        // Calculated properties
        public bool HasUpcomingAppointments => UpcomingAppointments?.Any() ?? false;
        public bool HasTodaysAppointments => TodaysAppointments?.Any() ?? false;
        public bool HasRecentMedicalHistories => RecentMedicalHistories?.Any() ?? false;
        public bool HasRecentPrescriptions => RecentPrescriptions?.Any() ?? false;

        [Display(Name = "Cancellation Rate")]
        [DisplayFormat(DataFormatString = "{0:P1}")]
        public double CancellationRate =>
            AppointmentsToday > 0 ?
            (AppointmentStatusSummary.TryGetValue(AppointmentStatus.Canceled, out var canceled) ?
                (double)canceled / AppointmentsToday : 0) : 0;
    }
}
