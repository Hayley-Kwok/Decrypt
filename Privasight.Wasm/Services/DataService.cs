using System.ComponentModel;
using System.Runtime.CompilerServices;
using Blazored.LocalStorage;
using Privasight.Model.Shared;
using Privasight.Model.Shared.Interfaces;

namespace Privasight.Wasm.Services;

public class DataService : INotifyPropertyChanged
{
    private readonly ILocalStorageService _localStorage;
    private CompanyRoot? _fbRoot;


    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public CompanyRoot? FbRoot
    {
        get => _fbRoot;
        private set
        {
            if (_fbRoot != null && _fbRoot.Equals(value)) return;
            _fbRoot = value;
            OnPropertyChanged();
        }
    }

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

        OnPropertyChanged(nameof(FbRoot));
        await _localStorage.SetItemAsync(nameof(FbRoot), FbRoot);
    }

    public async Task SetFbRootFromStorage()
    { 
        var storageData = await _localStorage.GetItemAsync<CompanyRoot>(nameof(FbRoot));
        FbRoot = storageData ?? new CompanyRoot();
    }

    public async Task RemoveFbData()
    {
        await _localStorage.RemoveItemAsync(nameof(FbRoot));
        FbRoot = new CompanyRoot();
    }
}