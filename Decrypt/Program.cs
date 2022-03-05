using Decrypt;
using Decrypt.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-GB");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-GB");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<DataService>();
builder.Services.AddScoped<ConfigService>();

await builder.Build().RunAsync();
