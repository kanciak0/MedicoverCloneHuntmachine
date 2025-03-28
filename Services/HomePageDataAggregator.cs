using MedicoverClone.Services.Interfaces;
using MedicoverClone.ViewModels;
using System.Threading.Tasks;

namespace MedicoverClone.Services
{
    public class HomePageDataAggregator : IHomePageDataAggregator
    {
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;
        private readonly IMedicalHistoryService _medicalHistoryService;

        public HomePageDataAggregator(
            IPatientService patientService,
            IAppointmentService appointmentService,
            IMedicalHistoryService medicalHistoryService)
        {
            _patientService = patientService;
            _appointmentService = appointmentService;
            _medicalHistoryService = medicalHistoryService;
        }

        public async Task<HomePageViewModel> GetHomePageDataAsync()
        {
            return new HomePageViewModel
            {
                TotalPatients = await _patientService.GetTotalPatientsAsync(),
                AppointmentsToday = await _appointmentService.GetTotalAppointmentsTodayAsync(),
                UpcomingAppointments = await _appointmentService.GetUpcomingAppointmentsAsync(),
                RecentMedicalHistories = await _medicalHistoryService.GetRecentMedicalHistoriesAsync()
            };
        }
    }
}
