using Blazored.LocalStorage;
using Privasight.Model.Shared.DataStructures;

namespace Privasight.Wasm.Services;

/// <summary>
/// Parent class for services using LocalStorage
/// </summary>
public abstract class ServiceUsingLocalStorage : INotifyPropertyChangedImplementation
{
    protected readonly ILocalStorageService LocalStorage;

    protected ServiceUsingLocalStorage(ILocalStorageService localStorage)
    {
        LocalStorage = localStorage;
    }
}