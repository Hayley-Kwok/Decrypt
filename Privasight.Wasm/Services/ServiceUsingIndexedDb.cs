using Microsoft.JSInterop;
using Newtonsoft.Json;
using Privasight.Model.Shared.DataStructures;

namespace Privasight.Wasm.Services;

/// <summary>
/// Parent class for services using IndexedDb
/// </summary>
public abstract class ServiceUsingIndexedDb : INotifyPropertyChangedImplementation
{
    protected readonly JsonSerializerSettings TypeAutoSettings;
    protected readonly IJSRuntime JsRuntime;

    protected ServiceUsingIndexedDb(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
        TypeAutoSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };
    }
}