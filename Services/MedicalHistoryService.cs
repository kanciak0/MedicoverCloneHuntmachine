using MedicoverClone.Domain;
using MedicoverClone.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicoverClone.Services
{
    public class MedicalHistoryService : IMedicalHistoryService
    {
        private readonly List<MedicalHistory> _medicalHistories;
        private readonly List<Patient> _patients;
        private int _nextId = 4; // Starting after initial mock data

        public MedicalHistoryService()
        {
            _patients = new List<Patient>
            {
                new Patient { PatientId = 1, FirstName = "John", LastName = "Doe" },
                new Patient { PatientId = 2, FirstName = "Jane", LastName = "Smith" }
            };

            _medicalHistories = new List<MedicalHistory>
            {
                new MedicalHistory {
                    HistoryId = 1,
                    DateOfVisit = DateTime.Now.AddDays(-3),
                    Diagnosis = "Hypertension",
                    Treatment = "Prescribed medication",
                    Patient = _patients[0]
                },
                new MedicalHistory {
                    HistoryId = 2,
                    DateOfVisit = DateTime.Now.AddDays(-5),
                    Diagnosis = "Type 2 Diabetes",
                    Treatment = "Diet and exercise plan",
                    Patient = _patients[1]
                },
                new MedicalHistory {
                    HistoryId = 3,
                    DateOfVisit = DateTime.Now.AddDays(-1),
                    Diagnosis = "Common Cold",
                    Treatment = "Rest and fluids",
                    Patient = _patients[0]
                }
            };

            // Link medical histories to patients
            foreach (var history in _medicalHistories)
            {
                history.Patient.MedicalHistories.Add(history);
            }
        }

        public Task<IEnumerable<MedicalHistory>> GetAllMedicalHistoriesAsync()
        {
            return Task.FromResult(_medicalHistories.AsEnumerable());
        }

        public Task<MedicalHistory> GetMedicalHistoryByIdAsync(int id)
        {
            return Task.FromResult(_medicalHistories.FirstOrDefault(h => h.HistoryId == id));
        }

        public Task AddMedicalHistoryAsync(MedicalHistory history)
        {
            history.HistoryId = _nextId++;
            history.Patient = _patients.FirstOrDefault(p => p.PatientId == history.Patient?.PatientId);
            _medicalHistories.Add(history);

            if (history.Patient != null)
            {
                history.Patient.MedicalHistories.Add(history);
            }

            return Task.CompletedTask;
        }

        public Task UpdateMedicalHistoryAsync(MedicalHistory history)
        {
            var existing = _medicalHistories.FirstOrDefault(h => h.HistoryId == history.HistoryId);
            if (existing != null)
            {
                // Remove from old patient
                existing.Patient?.MedicalHistories?.Remove(existing);

                // Update properties
                existing.DateOfVisit = history.DateOfVisit;
                existing.Diagnosis = history.Diagnosis;
                existing.Treatment = history.Treatment;
                existing.Notes = history.Notes;
                existing.Patient = _patients.FirstOrDefault(p => p.PatientId == history.Patient?.PatientId);

                // Add to new patient
                existing.Patient?.MedicalHistories?.Add(existing);
            }
            return Task.CompletedTask;
        }

        public Task DeleteMedicalHistoryAsync(int id)
        {
            var history = _medicalHistories.FirstOrDefault(h => h.HistoryId == id);
            if (history != null)
            {
                history.Patient?.MedicalHistories?.Remove(history);
                _medicalHistories.Remove(history);
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<MedicalHistory>> GetMedicalHistoriesByPatientAsync(int patientId)
        {
            return Task.FromResult(_medicalHistories
                .Where(h => h.Patient?.PatientId == patientId)
                .OrderByDescending(h => h.DateOfVisit)
                .AsEnumerable());
        }

        public Task<IEnumerable<MedicalHistory>> GetRecentMedicalHistoriesAsync(int count = 5)
        {
            return Task.FromResult(_medicalHistories
                .OrderByDescending(h => h.DateOfVisit)
                .Take(count)
                .AsEnumerable());
        }
    }
}