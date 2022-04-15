using Blazored.LocalStorage;
using Privasight.Model.Shared;
using Privasight.Model.Shared.Interfaces;

namespace Privasight.Wasm.Services;

public class DataService
{
    private readonly ILocalStorageService _localStorage;

    public CompanyRoot? FbRoot { get; private set; }

    public DataService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task LoadDataIntoStorage(Dictionary<string, IFileWrapper> newData)
    {
        if (FbRoot == null)
        {
            await SetFbRootFromStorage();
        }

        foreach (var (key, value) in newData)
        {
            if (FbRoot!.AvailableData.ContainsKey(key))
            {
                if (FbRoot.AvailableData[key] is ISingleItemListFile singleItemList)
                {
                    dynamic oldWrapper = singleItemList;
                    dynamic newWrapper = value;
                    if (oldWrapper.Items != null && newWrapper.Items != null)
                    {
                        oldWrapper.Items.AddRange(newWrapper.Items);
                    }
                    else if (oldWrapper.Items == null)
                    {
                        FbRoot.AvailableData[key] = value;
                    }
                }
            }
            else
            {
                FbRoot.AvailableData.Add(key, value);
            }
        }

        await _localStorage.SetItemAsync(nameof(FbRoot), FbRoot);
    }

    public async Task SetFbRootFromStorage()
    { 
        var storageData = await _localStorage.GetItemAsync<CompanyRoot>(nameof(FbRoot));
        FbRoot = storageData ?? new CompanyRoot();
    }
}