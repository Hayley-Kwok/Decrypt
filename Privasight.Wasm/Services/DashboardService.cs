using Blazored.LocalStorage;
using Privasight.Model.Shared.DataStructures.Dashboard;
using Privasight.Wasm.Configs;

namespace Privasight.Wasm.Services;

/// <summary>
/// Service for storing & accessing dashboard settings
/// </summary>
public class DashboardService : ServiceUsingLocalStorage
{
    public enum UpdateAction
    {
        Add,
        Update,
        Delete
    }

    private Dictionary<AvailableCompany, IList<DashboardSetting>>? _dashboardSettings;

    public Dictionary<AvailableCompany,IList<DashboardSetting>>? DashboardSettings
    {
        get => _dashboardSettings;
        private set
        {
            _dashboardSettings = value;
            OnPropertyChanged();
        }
    }

    private Dictionary<AvailableCompany, IEnumerable<string>>? _existingDashboardNames;
    public Dictionary<AvailableCompany, IEnumerable<string>>? ExistingDashboardNames
    {
        get
        {
            if (DashboardSettings == null) return null;

            if (_existingDashboardNames != null)
            {
                return _existingDashboardNames;
            }

            _existingDashboardNames = new Dictionary<AvailableCompany, IEnumerable<string>>();
            foreach (var (company, dashboardSettings) in DashboardSettings)
            {
                var names = dashboardSettings.Select(d => d.Name);
                _existingDashboardNames.Add(company, names);
            }

            return _existingDashboardNames;
        }
    }

    public DashboardService(ILocalStorageService localStorage) : base(localStorage)
    {
    }

    public async Task UpdateDashboardSettings(AvailableCompany company, DashboardSetting newSetting, UpdateAction action,
        DashboardSetting? oldSetting = null)
    {
        if (DashboardSettings == null)
        {
            await SetDashboardSettingsFromStorage();
        }

        if (!DashboardSettings!.ContainsKey(company))
        {
            DashboardSettings.Add(company, new List<DashboardSetting>());
        }

        switch (action)
        {
            case UpdateAction.Add:
                DashboardSettings![company].Add(newSetting);
                break;
            case UpdateAction.Update:
                DashboardSettings![company].Add(newSetting);
                if (oldSetting != null) DashboardSettings![company].Remove(oldSetting);
                break;
            case UpdateAction.Delete:
                DashboardSettings![company].Remove(newSetting);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }

        OnPropertyChanged(nameof(DashboardSettings));
        await LocalStorage.SetItemAsync(nameof(DashboardSettings), DashboardSettings);
    }

    public async Task SetDashboardSettingsFromStorage()
    {
        var dashboardSettings =
            await LocalStorage.GetItemAsync<Dictionary<AvailableCompany, IList<DashboardSetting>>>(nameof(DashboardSettings));
        DashboardSettings = dashboardSettings ?? new Dictionary<AvailableCompany, IList<DashboardSetting>>();
    }
}