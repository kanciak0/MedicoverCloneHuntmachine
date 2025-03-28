using MedicoverClone.Domain;
using MedicoverClone.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicoverClone.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly List<Doctor> _doctors;
        private int _nextId = 4;

        public DoctorService()
        {
            _doctors = new List<Doctor>
            {
                new Doctor {
                    DoctorId = 1,
                    FirstName = "Robert",
                    LastName = "Johnson",
                    Specialty = "Cardiology",
                    ContactInfo = "r.johnson@medicover.com"
                },
                new Doctor {
                    DoctorId = 2,
                    FirstName = "Emily",
                    LastName = "Williams",
                    Specialty = "Pediatrics",
                    ContactInfo = "e.williams@medicover.com"
                },
                new Doctor {
                    DoctorId = 3,
                    FirstName = "Michael",
                    LastName = "Brown",
                    Specialty = "Orthopedics",
                    ContactInfo = "m.brown@medicover.com"
                }
            };
        }

        public Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return Task.FromResult(_doctors.OrderBy(d => d.LastName).ThenBy(d => d.FirstName).ToList());
        }

        public Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return Task.FromResult(_doctors.FirstOrDefault(d => d.DoctorId == id));
        }

        public Task AddDoctorAsync(Doctor doctor)
        {
            doctor.DoctorId = _nextId++;
            _doctors.Add(doctor);
            return Task.CompletedTask;
        }

        public Task UpdateDoctorAsync(Doctor doctor)
        {
            var existingDoctor = _doctors.FirstOrDefault(d => d.DoctorId == doctor.DoctorId);
            if (existingDoctor != null)
            {
                existingDoctor.FirstName = doctor.FirstName;
                existingDoctor.LastName = doctor.LastName;
                existingDoctor.Specialty = doctor.Specialty;
                existingDoctor.ContactInfo = doctor.ContactInfo;
            }
            return Task.CompletedTask;
        }

        public Task DeleteDoctorAsync(int id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.DoctorId == id);
            if (doctor != null)
            {
                _doctors.Remove(doctor);
            }
            return Task.CompletedTask;
        }

        public Task<List<Doctor>> SearchDoctorsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllDoctorsAsync();

            return Task.FromResult(_doctors
                .Where(d => d.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                           d.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                           d.Specialty.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                           d.ContactInfo.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .OrderBy(d => d.LastName)
                .ThenBy(d => d.FirstName)
                .ToList());
        }

        public Task<List<string>> GetAllSpecialtiesAsync()
        {
            var specialties = new List<string>
            {
                "Cardiology",
                "Pediatrics",
                "Orthopedics",
                "Neurology",
                "Dermatology",
                "Oncology",
                "General Practice",
                "Surgery",
                "Radiology",
                "Psychiatry"
            };
            return Task.FromResult(specialties.OrderBy(s => s).ToList());
        }
    }
}