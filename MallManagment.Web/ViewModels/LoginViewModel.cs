using MallManagment.Web.IViewModels;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using MallManagment.Web.Providers;
using MallManagment.Web.Services;

namespace MallManagment.Web.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        private HttpClient _httpClient;
        private readonly NavigationManager _navManager;
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ITokenManagerService _tokenManager;

        public LoginViewModel()
        {

        }
        public LoginViewModel(HttpClient httpClient, NavigationManager navManager, AuthenticationStateProvider authStateProvider,
             ITokenManagerService tokenService)
        {
            _httpClient = httpClient;
            this._navManager = navManager;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authStateProvider = authStateProvider;
            _tokenManager = tokenService;
        }
        public bool ShowAuthError { get; set; }
        public LoginDto loginDto { get; set; }=new LoginDto();
        public string? Error { get; set; }

        public void CloseError()
        {
            ShowAuthError=false;
        }

        public async Task OnSubmitAsync()
        {
            var content = JsonSerializer.Serialize(loginDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _httpClient.PostAsync("api/account/login", bodyContent);
           
            if(!postResult.IsSuccessStatusCode) 
            { 
                throw new ArgumentNullException(nameof(postResult));
            }

            var postContent = await postResult.Content.ReadAsStringAsync();
            ResponseDto<AuthDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<AuthDto>>(postContent, _options);

            if (authResponse!.Status == ResponseStatus.Success)
            {
                var auth = authResponse.Model;
                var token = auth!.Token;
                await _tokenManager.SetAuth(authResponse.Model!);
                ((AuthStateProvider)_authStateProvider).Notify();

                _navManager.NavigateTo("/", true);
               
            }
            else
            {
                Console.WriteLine(postContent);
                ShowAuthError = true;
                Error = authResponse.Message;
            }
        }
    }
}
