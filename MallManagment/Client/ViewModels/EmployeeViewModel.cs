using MallManagment.Client.Components;
using MallManagment.Client.IViewModels;
using MallManagment.Client.Pages.Admin;
using MallManagment.Shared.Dtos;
using MallManagment.Shared.Enums;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;

namespace MallManagment.Client.ViewModels
{
    public class EmployeeViewModel : BaseViewModel, IEmployeeViewModel
    {
        public EmployeeDto? EmployeeDto { get; set; }
        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
        public EmployeeViewModel(HttpClient httpClient, IDialogService dialogService, ISnackbar snackbar)
            : base(httpClient, dialogService, snackbar) { } 

        public async Task LoadAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseDto<List<EmployeeDto>>>("api/Employees/list");
            if (response!.Status == ResponseStatus.Success)
                Employees = response.Model!;
        }
        public async Task DeleteAsync(EmployeeDto employee)
        {
            var parameters = new DialogParameters();
            parameters.Add("ConfirmMessage", $"Are you shure you want to delete {employee.FullName} account?");
            var dialog = await _dialogService.Show<ConfirmDialog>("Confirm to delete", parameters).Result;

            if (dialog.Data != null)
            {
                var deleteResponse = await _httpClient.DeleteAsync($"api/Employees/{employee.Id}");
                var putContent = await deleteResponse.Content.ReadAsStringAsync();
                ResponseDto<EmployeeDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<EmployeeDto>>(putContent, _options);

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
                    await LoadAsync();
                    _snackbar.Add($"{employee.FullName} is deleted successfully", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.DeleteForever;
                    });
                }
            }

        }
    }
}
