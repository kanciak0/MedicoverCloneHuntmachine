using MedicoverClone.ViewModels;
using System.Threading.Tasks;

namespace MedicoverClone.Services.Interfaces
{
    public interface IHomePageDataAggregator
    {
        Task<HomePageViewModel> GetHomePageDataAsync();
    }
}