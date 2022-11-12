using MallManagment.Web.Features;
using MallManagment.Models.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using MallManagment.Models.Dtos;
using System.Net.Http.Json;
using System.Security.Claims;
using MallManagment.Web.Services;

namespace MallManagment.Web.Providers
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenManagerService _tokenManager;
        private readonly NavigationManager navigationManager;
        private ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
        public AuthStateProvider(HttpClient httpClient, ITokenManagerService localStorage, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _tokenManager = localStorage;
            this.navigationManager = navigationManager;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenManager.GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                navigationManager.NavigateTo("/auth/login");
                return new AuthenticationState(claimsPrincipal);
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), AuthConstant.JWT_AUTH_TYPE)));
        }
        public void Notify()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
