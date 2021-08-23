using Application.Models;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ISettingsService
    {
        public Task Update(SettingsRequestModel settingsRequestModel);
        public Task<SettingsViewModel> Get();

        public string[] GetFileExtensions();
    }
}
