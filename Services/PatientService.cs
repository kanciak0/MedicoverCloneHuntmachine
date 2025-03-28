using MedicoverClone.Domain;
using MedicoverClone.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicoverClone.Services
{
    public class PatientService : IPatientService
    {
        private readonly List<Patient> _patients;
        private int _nextId = 4; // Starting after our initial mock data

        public PatientService()
        {
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
                },
                new Patient {
                    PatientId = 3,
                    FirstName = "Michael",
                    LastName = "Brown",
                    DateOfBirth = new DateTime(1990, 3, 10),
                    Gender = "Male",
                    ContactInfo = "michael.brown@example.com",
                    Address = "789 Pine Rd"
                }
            };
        }

        public Task<List<Patient>> GetAllPatientsAsync()
        {
            return Task.FromResult(_patients.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToList());
        }

        public Task<Patient> GetPatientByIdAsync(int id)
        {
            return Task.FromResult(_patients.FirstOrDefault(p => p.PatientId == id));
        }

        public Task AddPatientAsync(Patient patient)
        {
            patient.PatientId = _nextId++;
            _patients.Add(patient);
            return Task.CompletedTask;
        }

        public Task UpdatePatientAsync(Patient patient)
        {
            var existingPatient = _patients.FirstOrDefault(p => p.PatientId == patient.PatientId);
            if (existingPatient != null)
            {
                existingPatient.FirstName = patient.FirstName;
                existingPatient.LastName = patient.LastName;
                existingPatient.DateOfBirth = patient.DateOfBirth;
                existingPatient.Gender = patient.Gender;
                existingPatient.ContactInfo = patient.ContactInfo;
                existingPatient.Address = patient.Address;
            }
            return Task.CompletedTask;
        }

        public Task DeletePatientAsync(int id)
        {
            var patient = _patients.FirstOrDefault(p => p.PatientId == id);
            if (patient != null)
            {
                _patients.Remove(patient);
            }
            return Task.CompletedTask;
        }

        public Task<int> GetTotalPatientsAsync()
        {
            return Task.FromResult(_patients.Count);
        }

        public Task<List<Patient>> SearchPatientsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllPatientsAsync();

            return Task.FromResult(_patients
                .Where(p => p.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             p.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             p.ContactInfo.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToList());
        }
    }
}