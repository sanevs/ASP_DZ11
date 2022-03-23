using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebAssemblyKW12;
using BlazorWebAssemblyKW12.Data;
using Glory.Domain;
using ShopClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<ICatalog, Catalog>();
builder.Services.AddSingleton<ICart, Cart>();
builder.Services.AddSingleton<Categories>();

builder.Services.AddSingleton(new ClientDTO("http://localhost:5194", new ()));
await builder.Build().RunAsync();