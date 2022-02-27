using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebAssemblyKW12;
using BlazorWebAssemblyKW12.Data;
using Glory.Domain;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddSingleton<ICatalog, Catalog>();
builder.Services.AddSingleton<ICart, Cart>();
builder.Services.AddSingleton<Categories>();

await builder.Build().RunAsync();