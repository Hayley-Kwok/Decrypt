using Blazored.LocalStorage;
using Privasight.Model.Shared;
using Privasight.Model.Shared.Interfaces;

namespace Privasight.Wasm.Services;

public class DataService
{
    private CompanyRoot? _fbRoot;
    private readonly ISyncLocalStorageService _localStorage;

    public CompanyRoot? FbRoot
    {
        get => _fbRoot ??= GetFbRootFromStorage();
        private set => _fbRoot = value;
    }

    public DataService(ISyncLocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public void LoadDataIntoStorage(Dictionary<string, IFileWrapper> newData)
    {
        foreach (var (key, value) in newData)
        {
            if (FbRoot.AvailableData.ContainsKey(key))
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

        _localStorage.SetItem(nameof(FbRoot), FbRoot);
    }

    private CompanyRoot GetFbRootFromStorage()
    {
        var storageData = _localStorage.GetItem<CompanyRoot>(nameof(FbRoot));

        return storageData ?? new CompanyRoot();
    }
}