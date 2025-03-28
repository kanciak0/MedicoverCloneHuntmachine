using MedicoverClone.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicoverClone.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task AddAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int id);
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date);
        Task<List<TimeSpan>> GetAvailableTimeSlotsAsync(int doctorId, DateTime date);
        Task<int> GetTotalAppointmentsTodayAsync();
        Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync(int count = 5);
        Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime date, TimeSpan time);
        Task<List<Appointment>> SearchAppointmentsAsync(string searchTerm);
        Task UpdateAppointmentAsync(Appointment appointment);
    }
}