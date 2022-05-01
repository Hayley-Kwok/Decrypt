using Microsoft.JSInterop;
using Newtonsoft.Json;
using Privasight.Model.Shared.DataStructures.Interfaces;
using Privasight.Wasm.Configs;

namespace Privasight.Wasm.Services;

/// <summary>
/// Service for storing & accessing companies data
/// </summary>
public class CompanyDataService : ServiceUsingIndexedDb
{
    public string DataLoadingStatus { get; set; } = "";
    public bool LoadingData { get; set; }

    /// <summary>
    /// Key: the company
    /// Value: Store the transformed FileWrappers (Json object that has transformed to supported C# object)
    /// 1 FileWrapper correspond to 1 Json File in the zip file
    ///     Key: Name of the type of the FileWrapper stored in value; Value: The FileWrapper Object
    /// </summary>
    private Dictionary<AvailableCompany, Dictionary<string, IFileWrapper>>? _availableData;

    public Dictionary<AvailableCompany, Dictionary<string, IFileWrapper>>? AvailableData
    {
        get => _availableData;
        private set
        {
            _availableData = value;
            OnPropertyChanged();
        }
    }

    public CompanyDataService(IJSRuntime jsRuntime) : base(jsRuntime)
    {
    }

    public async Task UpdateAvailableData(AvailableCompany company, Dictionary<string, IFileWrapper> newData)
    {
        if (AvailableData == null)
        {
            await SetAvailableDataFromStorage();
        }

        if (!AvailableData!.ContainsKey(company))
        {
            AvailableData.Add(company, new Dictionary<string, IFileWrapper>());
        }

        foreach (var (key, value) in newData)
        {
            if (AvailableData[company].ContainsKey(key))
            {
                if (AvailableData[company][key] is ISingleItemListFile singleItemList)
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
                        AvailableData[company][key] = value;
                    }
                }
            }
            else
            {
                AvailableData[company].Add(key, value);
            }
        }

        await UpdateAvailableDataInStorage();
    }

    public async Task SetAvailableDataFromStorage()
    {
        var dataStr = await JsRuntime.InvokeAsync<string>("getCompanyData");
        if (string.IsNullOrWhiteSpace(dataStr))
        {
            AvailableData = new Dictionary<AvailableCompany, Dictionary<string, IFileWrapper>>();
        }

        AvailableData =
            JsonConvert.DeserializeObject<Dictionary<AvailableCompany, Dictionary<string, IFileWrapper>>>(dataStr,
                TypeAutoSettings) ??
            new Dictionary<AvailableCompany, Dictionary<string, IFileWrapper>>();
    }

    public async Task RemoveAvailableData(AvailableCompany company)
    {
        if (AvailableData == null) return;

        AvailableData[company] = new Dictionary<string, IFileWrapper>();
        await UpdateAvailableDataInStorage();
    }

    private async Task UpdateAvailableDataInStorage()
    {
        var jsonStr = JsonConvert.SerializeObject(AvailableData, TypeAutoSettings);
        await JsRuntime.InvokeVoidAsync("setCompanyData", jsonStr);
        OnPropertyChanged(nameof(AvailableData));
    }
}