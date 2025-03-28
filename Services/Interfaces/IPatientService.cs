using MedicoverClone.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicoverClone.Services.Interfaces
{
    public interface IPatientService
    {
        Task AddPatientAsync(Patient patient);
        Task DeletePatientAsync(int id);
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task<int> GetTotalPatientsAsync();
        Task<List<Patient>> SearchPatientsAsync(string searchTerm);
        Task UpdatePatientAsync(Patient patient);
    }
}