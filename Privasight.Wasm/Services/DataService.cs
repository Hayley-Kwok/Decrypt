using System.ComponentModel;
using System.Runtime.CompilerServices;
using Blazored.LocalStorage;
using Privasight.Model.Shared.DataStructures.Interfaces;

namespace Privasight.Wasm.Services;

public partial class DataService : INotifyPropertyChanged
{
    private readonly ILocalStorageService _localStorage;

    #region PropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    public string DataLoadingStatus { get; set; } = "";
    public bool LoadingData { get; set; }

    /// <summary>
    /// Key: the company
    /// Value: Store the transformed FileWrappers (Json object that has transformed to supported C# object)
    /// 1 FileWrapper correspond to 1 Json File in the zip file
    ///     Key: Name of the type of the FileWrapper stored in value; Value: The FileWrapper Object
    /// </summary>
    private Dictionary<AvailableCompany, Dictionary<string, IFileWrapper>>? _companyAvailableData;

    public Dictionary<AvailableCompany, Dictionary<string, IFileWrapper>>? CompanyAvailableData
    {
        get => _companyAvailableData;
        private set
        {
            _companyAvailableData = value;
            OnPropertyChanged();
        }
    }

    public DataService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task UpdateCompanyAvailableData(AvailableCompany company, Dictionary<string, IFileWrapper> newData)
    {
        if (CompanyAvailableData == null)
        {
            await SetCompanyAvailableDataFromStorage();
        }

        if (!CompanyAvailableData!.ContainsKey(company))
        {
            CompanyAvailableData.Add(company, new Dictionary<string, IFileWrapper>());
        }

        foreach (var (key, value) in newData)
        {
            if (CompanyAvailableData[company].ContainsKey(key))
            {
                if (CompanyAvailableData[company][key] is ISingleItemListFile singleItemList)
                {
                    //todo using dynamic is not the best way to do this but cannot think of any better way at the moment
                    dynamic oldWrapper = singleItemList;
                    dynamic newWrapper = value;
                    if (oldWrapper.Items != null && newWrapper.Items != null)
                    {
                        oldWrapper.Items.AddRange(newWrapper.Items);
                    }
                    else if (oldWrapper.Items == null)
                    {
                        CompanyAvailableData[company][key] = value;
                    }
                }
            }
            else
            {
                CompanyAvailableData[company].Add(key, value);
            }
        }

        OnPropertyChanged(nameof(CompanyAvailableData));
        await _localStorage.SetItemAsync(nameof(CompanyAvailableData), CompanyAvailableData);
    }

    public async Task SetCompanyAvailableDataFromStorage()
    {
        var storageData =
            await _localStorage.GetItemAsync<Dictionary<AvailableCompany, Dictionary<string, IFileWrapper>>>(
                nameof(CompanyAvailableData));
        CompanyAvailableData = storageData ?? new Dictionary<AvailableCompany, Dictionary<string, IFileWrapper>>();
    }

    public async Task RemoveCompanyData(AvailableCompany company)
    {
        if (CompanyAvailableData == null) return;

        CompanyAvailableData[company] = new Dictionary<string, IFileWrapper>();
        await _localStorage.SetItemAsync(nameof(CompanyAvailableData), CompanyAvailableData);
        OnPropertyChanged(nameof(CompanyAvailableData));
    }
}