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

        private HashSet<DashboardSetting>? _fbDashboardSettings;

        public HashSet<DashboardSetting>? FbDashboardSettings
        {
            get => _fbDashboardSettings;
            private set
            {
                if (_fbDashboardSettings != null && _fbDashboardSettings.Equals(value)) return;
                _fbDashboardSettings = value;
                OnPropertyChanged();
            }
        }

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
                await _localStorage.GetItemAsync<HashSet<DashboardSetting>>(nameof(FbDashboardSettings));
            FbDashboardSettings = dashboardSettings ?? new HashSet<DashboardSetting>();
        }

        public async Task ClearFbDashboardSettings()
        {
            await _localStorage.RemoveItemAsync(nameof(FbDashboardSettings));
            FbDashboardSettings = new HashSet<DashboardSetting>();
        }
    }
}