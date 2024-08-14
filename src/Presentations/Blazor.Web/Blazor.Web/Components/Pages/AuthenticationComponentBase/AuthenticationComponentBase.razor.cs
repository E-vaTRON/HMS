using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Blazor.Web.Components.Pages.AuthenticationComponentBase
{
    public partial class AuthenticationComponentBase
    {
        [Inject]
        protected ILocalStorageService LocalStorage { get; set; } = null!;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
        }
        protected virtual async Task<bool> IsNotLogin()
        {
            var token = await this.LocalStorage.GetItemAsStringAsync("token");
            if (string.IsNullOrEmpty(token))
            {
                return true;
            }
            return false;
        }
    }
}