using Privasight.Wasm;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Privasight.Wasm.Services;
using Radzen;
using System.Globalization;
using Blazored.LocalStorage;
using Blazored.LocalStorage.Serialization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Privasight.Wasm.Data;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-GB");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-GB");
JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
{
    TypeNameHandling = TypeNameHandling.Auto
};

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DataService>();
builder.Services.AddScoped<ConfigService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.Replace(ServiceDescriptor.Scoped<IJsonSerializer, NewtonSoftJsonSerializer>());

await builder.Build().RunAsync();
