using MallManagment.Client.Components;
using MallManagment.Client.IViewModels;
using MallManagment.Shared.Dtos;
using MallManagment.Shared.Enums;
using MallManagment.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;

namespace MallManagment.Client.ViewModels
{
    public class EmailConfirmViewModel : BaseViewModel, IEmailConfirmViewModel
    {
        public NavigationManager _navigationManager { get; }
        public EmailConfirmViewModel(HttpClient httpClient, IDialogService dialogService, ISnackbar snackbar,NavigationManager navigationManager)
        : base(httpClient, dialogService, snackbar)
        {
            _navigationManager = navigationManager;
        }

        public ConfirmPasswordDto? EmailConfirmDto { get; set; }
        public ConfirmDto ConfirmDto { get; set; } = new ();

        public async Task LoadAsync()
        {
            var response = await _httpClient.PostAsJsonAsync<ConfirmDto>("api/account/emailconfirm",ConfirmDto);
            var putContent = await response.Content.ReadAsStringAsync();
           
            ResponseDto<ConfirmPasswordDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<ConfirmPasswordDto>>(putContent, _options);

            if (authResponse!.Status == ResponseStatus.Success)
            {
                EmailConfirmDto = authResponse.Model!;
            }
            else
            {
                _snackbar.Add($"Error occured : {authResponse.Message}", Severity.Warning, config =>
                {
                    config.Icon = Icons.Filled.Warning;
                });
            }
        }
        public async Task SetPasswordAsync()
        {
            var response = await _httpClient.PostAsJsonAsync<ConfirmPasswordDto>("api/account/setpassword", EmailConfirmDto);
            
            var putContent = await response.Content.ReadAsStringAsync();
            ResponseDto<ConfirmPasswordDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<ConfirmPasswordDto>>(putContent, _options);

            if (authResponse!.Status != ResponseStatus.Success)
            {
                Console.WriteLine(putContent);
                _snackbar.Add($"Unable to perform the current request", Severity.Warning, config =>
                {
                    config.Icon = Icons.Filled.Warning;
                });
            }
            else
            {
                _navigationManager.NavigateTo("/auth/login");
            }
        }
    }
}
