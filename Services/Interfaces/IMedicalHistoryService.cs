using MedicoverClone.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicoverClone.Services.Interfaces
{
    public interface IMedicalHistoryService
    {
        Task AddMedicalHistoryAsync(MedicalHistory history);
        Task DeleteMedicalHistoryAsync(int id);
        Task<IEnumerable<MedicalHistory>> GetAllMedicalHistoriesAsync();
        Task<IEnumerable<MedicalHistory>> GetMedicalHistoriesByPatientAsync(int patientId);
        Task<MedicalHistory> GetMedicalHistoryByIdAsync(int id);
        Task<IEnumerable<MedicalHistory>> GetRecentMedicalHistoriesAsync(int count = 5);
        Task UpdateMedicalHistoryAsync(MedicalHistory history);
    }
}