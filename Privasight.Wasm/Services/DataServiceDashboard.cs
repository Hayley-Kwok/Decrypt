using Privasight.Model.Shared.DataStructures.Dashboard;

namespace Privasight.Wasm.Services
{
    public partial class DataService
    {
        private List<DashboardSetting>? _fbDashboardSettings;

        public List<DashboardSetting>? FbDashboardSettings
        {
            get => _fbDashboardSettings;
            private set
            {
                if(_fbDashboardSettings!= null && _fbDashboardSettings.Equals(value)) return;
                _fbDashboardSettings = value;
                OnPropertyChanged();
            }
        }

        public async Task UpdateFbDashboardSettings(IEnumerable<DashboardSetting> newSettings)
        {
            if (FbDashboardSettings == null)
            {
                await SetFbDashboardsFromStorage();
            }

            FbDashboardSettings!.AddRange(newSettings);
            await _localStorage.SetItemAsync(nameof(FbDashboardSettings), FbDashboardSettings);
        }

        public async Task SetFbDashboardsFromStorage()
        {
            var dashboardSettings = await _localStorage.GetItemAsync<List<DashboardSetting>>(nameof(FbDashboardSettings));
            FbDashboardSettings = dashboardSettings ?? new List<DashboardSetting>();
        }

        public async Task RemoveFbDashboardSettings()
        {
            await _localStorage.RemoveItemAsync(nameof(FbDashboardSettings));
            FbDashboardSettings = new List<DashboardSetting>();
        }
    }
}
