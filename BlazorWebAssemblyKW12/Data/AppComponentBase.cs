using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using ShopClient;

namespace BlazorWebAssemblyKW12.Data;

public abstract class AppComponentBase : ComponentBase
{
    [Inject] protected ClientDTO _client { get; private set; }
    [Inject] protected ILocalStorageService _storageService { get; private set; }
    
    protected bool IsTokenChecked { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (!IsTokenChecked)
        {
            IsTokenChecked = true;
            var token = await _storageService.GetItemAsync<string>("token");
            if(!string.IsNullOrEmpty(token))
                _client.SetToken(token);
        }
    }

    protected async Task SetToken(string token) =>
        await _storageService.SetItemAsStringAsync("token", token);
    
    // protected async Task Logout() => 
    //     await _storageService.SetItemAsStringAsync("token", "");
}