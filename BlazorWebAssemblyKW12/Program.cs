using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebAssemblyKW12;
using BlazorWebAssemblyKW12.Data;
using Glory.Domain;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddSingleton<ICatalog, Catalog>();
builder.Services.AddSingleton<ICart, Cart>();
builder.Services.AddSingleton<Categories>();
//builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<IList<ProductDTO>, List<ProductDTO>>( (s) =>
{
    var client = new HttpClient();
    return client.GetFromJsonAsync<List<ProductDTO>>("http://localhost:5194/products").Result;
});


await builder.Build().RunAsync();