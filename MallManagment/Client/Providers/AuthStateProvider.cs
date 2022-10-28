using MallManagment.Client.Features;
using MallManagment.Shared.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using MallManagment.Shared.Dtos;
using System.Net.Http.Json;
using System.Security.Claims;

namespace MallManagment.Client.Providers
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        public AuthStateProvider(HttpClient httpClient,  NavigationManager navigationManager)
        {
            _httpClient = httpClient;
        }
        public async override  Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthDto auth = await _httpClient.GetFromJsonAsync<AuthDto>("api/account/currentuser");
            if(auth!=null && auth.EmployeeId != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,auth.EmployeeId),
                    new Claim(ClaimTypes.Name,auth.FullName),
                    new Claim(ClaimTypes.Email,auth.Email),
                    new Claim(ClaimTypes.Role,auth.StringRole),
                };
                var claimIdentity = new ClaimsIdentity(claims, "serverAuth");
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                return new AuthenticationState(claimPrincipal);
            }
            else
                 return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        public void Notify()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
