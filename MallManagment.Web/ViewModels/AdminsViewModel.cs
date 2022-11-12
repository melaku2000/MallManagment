using MallManagment.Web.Components;
using MallManagment.Web.IViewModels;
using MallManagment.Web.Pages.Admin;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using MallManagment.Models.Extenssions;
using MallManagment.Models.Entities;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;

namespace MallManagment.Web.ViewModels
{
    public class AdminsViewModel : BaseViewModel, IAdminsViewModel
    {
        public AdminstratorDto? AdminDto { get; set; }
        public List<AdminstratorDto> Admins { get; set; } = new List<AdminstratorDto>();
        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
        public AdminsViewModel(HttpClient httpClient, IDialogService dialogService, ISnackbar snackbar)
            : base(httpClient, dialogService, snackbar) { } 

        public async Task LoadAsync()
        {
            var adminResponse = await _httpClient.GetFromJsonAsync<ResponseDto<List<AdminstratorDto>>>("api/admins/list");
            if (adminResponse!.Status == ResponseStatus.Success)
                Admins = adminResponse.Model!;
            var employeeResponse = await _httpClient.GetFromJsonAsync<ResponseDto<List<EmployeeDto>>>("api/Employees/list");
            if (employeeResponse!.Status == ResponseStatus.Success)
            {
                var employees = employeeResponse.Model!;
                Employees = employees.Where(a => Admins.Any(x => x.EmployeeId != a.Id)).ToList();
            }
        }
        public async Task CreateAsync()
        {
            var parameters = new DialogParameters();
            var adminDto = new AdminstratorDto();

            parameters.Add("admin", adminDto);
            parameters.Add("employees", Employees);
            var dialog = await _dialogService.Show<AdminDialog>("Add admin", parameters).Result;

            if (dialog.Data != null)
            {
                AdminstratorDto admin = dialog.Data as AdminstratorDto;

                var response = await _httpClient.PostAsJsonAsync<AdminstratorDto>("api/admins", admin);
                var putContent = await response.Content.ReadAsStringAsync();
                ResponseDto<AdminstratorDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<AdminstratorDto>>(putContent, _options);

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
                    var addAdmin = authResponse.Model;

                    _snackbar.Add($"{addAdmin!.FullName} is added to {addAdmin.Role.GetStringValue()} role.", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.Info;
                    });
                    await LoadAsync();
                }
            }
        }
        public async Task UpdateAsync(AdminstratorDto adminDto)
        {
            var parameters = new DialogParameters();
            parameters.Add("admin", adminDto);
            parameters.Add("employees", Employees);
            var dialog = await _dialogService.Show<AdminDialog>("Edit admin", parameters).Result;

            if (dialog.Data != null)
            {
                AdminstratorDto admin = dialog.Data as AdminstratorDto;

                var response = await _httpClient.PutAsJsonAsync<AdminstratorDto>($"api/admins/{admin.EmployeeId}", admin);
                var putContent = await response.Content.ReadAsStringAsync();
                ResponseDto<AdminstratorDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<AdminstratorDto>>(putContent, _options);

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
                    var addAdmin = authResponse.Model;

                    _snackbar.Add($"{addAdmin!.FullName} is updated to {addAdmin.Role.GetStringValue()} role.", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.Info;
                    });
                    await LoadAsync();
                }
            }
        }
        public async Task DeleteAsync(AdminstratorDto employee)
        {
            var parameters = new DialogParameters();
            parameters.Add("ConfirmMessage", $"Are you shure you want to delete {employee.FullName} account?");
            var dialog = await _dialogService.Show<ConfirmDialog>("Confirm to delete", parameters).Result;

            if (dialog.Data != null)
            {
                var deleteResponse = await _httpClient.DeleteAsync($"api/admins/{employee.EmployeeId}");
                var putContent = await deleteResponse.Content.ReadAsStringAsync();
                ResponseDto<AdminstratorDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<AdminstratorDto>>(putContent, _options);

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
