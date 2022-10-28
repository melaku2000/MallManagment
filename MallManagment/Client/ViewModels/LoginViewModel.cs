using MallManagment.Client.IViewModels;
using MallManagment.Shared.Dtos;
using MallManagment.Shared.Enums;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Text.Json;

namespace MallManagment.Client.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        private HttpClient _httpClient;
        private readonly NavigationManager _navManager;
        private readonly JsonSerializerOptions _options;

        public LoginViewModel()
        {

        }
        public LoginViewModel(HttpClient httpClient, NavigationManager navManager)
        {
            _httpClient = httpClient;
            this._navManager = navManager;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
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
            var postContent = await postResult.Content.ReadAsStringAsync();
            ResponseDto<AuthDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<AuthDto>>(postContent, _options);

            if (authResponse.Status != ResponseStatus.Success)
            {
                Console.WriteLine(postContent);
                ShowAuthError = true;
                Error = authResponse.Message;
            }
            else
                _navManager.NavigateTo("/", true);
        }
    }
}
