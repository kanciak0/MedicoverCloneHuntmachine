using MedicoverClone.Domain;
using MedicoverClone.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicoverClone.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly List<Appointment> _appointments;
        private readonly List<Patient> _patients;
        private readonly List<Doctor> _doctors;
        private int _nextId = 4; // Starting after initial mock data

        public AppointmentService()
        {
            // Initialize patients
            _patients = new List<Patient>
            {
                new Patient {
                    PatientId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1980, 5, 15),
                    Gender = "Male",
                    ContactInfo = "john.doe@example.com",
                    Address = "123 Main St"
                },
                new Patient {
                    PatientId = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1975, 8, 22),
                    Gender = "Female",
                    ContactInfo = "jane.smith@example.com",
                    Address = "456 Oak Ave"
                }
            };

            // Initialize doctors
            _doctors = new List<Doctor>
            {
                new Doctor {
                    DoctorId = 1,
                    FirstName = "Robert",
                    LastName = "Johnson",
                    Specialty = "Cardiology",
                    ContactInfo = "r.johnson@clinic.com"
                },
                new Doctor {
                    DoctorId = 2,
                    FirstName = "Emily",
                    LastName = "Williams",
                    Specialty = "Pediatrics",
                    ContactInfo = "e.williams@clinic.com"
                }
            };

            // Initialize appointments
            _appointments = new List<Appointment>
            {
                new Appointment {
                    AppointmentId = 1,
                    Date = DateTime.Today.AddHours(10),
                    AppointmentStatus = AppointmentStatus.Pending,
                    Patient = _patients[0],
                    Doctor = _doctors[0],
                    Reason = "Annual checkup",
                    Notes = "Patient has high blood pressure history"
                },
                new Appointment {
                    AppointmentId = 2,
                    Date = DateTime.Today.AddHours(14),
                    AppointmentStatus = AppointmentStatus.Pending,
                    Patient = _patients[1],
                    Doctor = _doctors[1],
                    Reason = "Vaccination",
                    Notes = "Child's regular vaccination schedule"
                },
                new Appointment {
                    AppointmentId = 3,
                    Date = DateTime.Today.AddDays(1).AddHours(11),
                    AppointmentStatus = AppointmentStatus.Pending,
                    Patient = _patients[0],
                    Doctor = _doctors[1],
                    Reason = "Follow-up",
                    Notes = "Follow up on previous treatment"
                }
            };

            // Link appointments to patients and doctors
            foreach (var appointment in _appointments)
            {
                appointment.Patient.Appointments.Add(appointment);
                appointment.Doctor.Appointments.Add(appointment);
            }
        }

        // Your existing methods
        public Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync(int count = 5)
        {
            return Task.FromResult(_appointments
                .Where(a => a.Date >= DateTime.Now && a.AppointmentStatus == AppointmentStatus.Pending)
                .OrderBy(a => a.Date)
                .Take(count)
                .AsEnumerable());
        }

        public Task<int> GetTotalAppointmentsTodayAsync()
        {
            return Task.FromResult(_appointments.Count(a => a.Date.Date == DateTime.Today && a.AppointmentStatus != AppointmentStatus.Canceled));
        }

        // New methods for full CRUD operations
        public Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return Task.FromResult(_appointments
                .OrderBy(a => a.Date)
                .ToList());
        }

        public Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return Task.FromResult(_appointments.FirstOrDefault(a => a.AppointmentId == id));
        }

        public Task AddAppointmentAsync(Appointment appointment)
        {
            appointment.AppointmentId = _nextId++;
            _appointments.Add(appointment);

            // Link to patient and doctor
            appointment.Patient.Appointments.Add(appointment);
            appointment.Doctor.Appointments.Add(appointment);

            return Task.CompletedTask;
        }

        public Task UpdateAppointmentAsync(Appointment appointment)
        {
            var existing = _appointments.FirstOrDefault(a => a.AppointmentId == appointment.AppointmentId);
            if (existing != null)
            {
                // Remove from old patient and doctor
                existing.Patient.Appointments.Remove(existing);
                existing.Doctor.Appointments.Remove(existing);

                // Update details
                existing.Date = appointment.Date;
                existing.Patient = appointment.Patient;
                existing.Doctor = appointment.Doctor;
                existing.AppointmentStatus = appointment.AppointmentStatus;
                existing.Reason = appointment.Reason;
                existing.Notes = appointment.Notes;

                // Add to new patient and doctor
                existing.Patient.Appointments.Add(existing);
                existing.Doctor.Appointments.Add(existing);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAppointmentAsync(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.AppointmentId == id);
            if (appointment != null)
            {
                // Remove from patient and doctor
                appointment.Patient.Appointments.Remove(appointment);
                appointment.Doctor.Appointments.Remove(appointment);

                // Remove from list
                _appointments.Remove(appointment);
            }
            return Task.CompletedTask;
        }

        public Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            return Task.FromResult(_appointments
                .Where(a => a.Date.Date == date.Date)
                .OrderBy(a => a.Date)
                .ToList());
        }

        public Task<List<TimeSpan>> GetAvailableTimeSlotsAsync(int doctorId, DateTime date)
        {
            // Standard business hours
            var startTime = new TimeSpan(9, 0, 0); // 9 AM
            var endTime = new TimeSpan(17, 0, 0);  // 5 PM
            var slotDuration = TimeSpan.FromMinutes(30);

            var bookedSlots = _appointments
                .Where(a => a.Doctor.DoctorId == doctorId && a.Date.Date == date.Date)
                .Select(a => a.Date.TimeOfDay)
                .ToList();

            var availableSlots = new List<TimeSpan>();
            for (var time = startTime; time < endTime; time = time.Add(slotDuration))
            {
                if (!bookedSlots.Contains(time))
                {
                    availableSlots.Add(time);
                }
            }

            return Task.FromResult(availableSlots);
        }

        public Task<List<Appointment>> SearchAppointmentsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllAppointmentsAsync();

            return Task.FromResult(_appointments
                .Where(a => a.Patient.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             a.Patient.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             a.Doctor.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             a.Doctor.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             a.Reason.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .OrderBy(a => a.Date)
                .ToList());
        }

        public Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime date, TimeSpan time)
        {
            return Task.FromResult(!_appointments.Any(a =>
                a.Doctor.DoctorId == doctorId &&
                a.Date.Date == date.Date &&
                a.Date.TimeOfDay == time));
        }
    }
}