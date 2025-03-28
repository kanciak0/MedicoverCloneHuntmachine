using MedicoverClone.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicoverClone.Services.Interfaces
{
    public interface IDoctorService
    {
        Task AddDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(int id);
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<List<string>> GetAllSpecialtiesAsync();
        Task<Doctor> GetDoctorByIdAsync(int id);
        Task<List<Doctor>> SearchDoctorsAsync(string searchTerm);
        Task UpdateDoctorAsync(Doctor doctor);
    }
}