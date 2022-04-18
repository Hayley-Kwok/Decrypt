using Privasight.Model.Shared.DataStructures.Dashboard;

namespace Privasight.Wasm.Services
{
    public partial class DataService
    {
        public enum UpdateAction
        {
            Add,
            Update,
            Delete
        }

        private ICollection<DashboardSetting>? _fbDashboardSettings;

        public ICollection<DashboardSetting>? FbDashboardSettings
        {
            get => _fbDashboardSettings;
            private set
            {
                _fbDashboardSettings = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<string>? FbExistingDashboardNames => FbDashboardSettings?.Select(d => d.Name);

        public async Task UpdateFbDashboardSettings(DashboardSetting newSetting, UpdateAction action,
            DashboardSetting? oldSetting = null)
        {
            if (FbDashboardSettings == null)
            {
                await SetFbDashboardsFromStorage();
            }

            switch (action)
            {
                case UpdateAction.Add:
                    FbDashboardSettings!.Add(newSetting);
                    break;
                case UpdateAction.Update:
                    FbDashboardSettings!.Add(newSetting);
                    if (oldSetting != null) FbDashboardSettings!.Remove(oldSetting);
                    break;
                case UpdateAction.Delete:
                    FbDashboardSettings!.Remove(newSetting);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

            OnPropertyChanged(nameof(FbDashboardSettings));
            await _localStorage.SetItemAsync(nameof(FbDashboardSettings), FbDashboardSettings);
        }

        public async Task SetFbDashboardsFromStorage()
        {
            var dashboardSettings =
                await _localStorage.GetItemAsync<ICollection<DashboardSetting>>(nameof(FbDashboardSettings));
            FbDashboardSettings = dashboardSettings ?? new List<DashboardSetting>();
        }

        public async Task ClearFbDashboardSettings()
        {
            await _localStorage.RemoveItemAsync(nameof(FbDashboardSettings));
            FbDashboardSettings = new List<DashboardSetting>();
        }
    }
}