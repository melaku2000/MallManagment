using MallManagment.Web.Components;
using MallManagment.Web.IViewModels;
using MallManagment.Web.Pages.Admin;
using MallManagment.Web.Pages.Employee;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using MallManagment.Models.Extenssions;
using MallManagment.Models.Entities;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;
using MallManagment.Web.ViewModels;

namespace MallManagment.Web.IViewModels
{
    public class CustomerViewModel : BaseViewModel, ICustomerViewModel
    {
        public CustomerDto? customerDto { get; set; }
        public List<CustomerDto> Customers { get; set; } = new List<CustomerDto>();
        public List<CatagoryDto> Catagories { get; set; } = new List<CatagoryDto>();
        public CustomerViewModel(HttpClient httpClient, IDialogService dialogService, ISnackbar snackbar)
            : base(httpClient, dialogService, snackbar) { } 

        public async Task LoadAsync()
        {
            var adminResponse = await _httpClient.GetFromJsonAsync<ResponseDto<List<CustomerDto>>>("api/Customers/list");
            if (adminResponse!.Status == ResponseStatus.Success)
                Customers = adminResponse.Model!;
            var catagoryResponse = await _httpClient.GetFromJsonAsync<ResponseDto<List<CatagoryDto>>>("api/Catagories/list");
            if (catagoryResponse!.Status == ResponseStatus.Success)
                Catagories = catagoryResponse.Model!;
        }
        public async Task CreateAsync()
        {
            var parameters = new DialogParameters();
            var customerDto = new CustomerDto();

            parameters.Add("customer", customerDto);
            parameters.Add("catagories", Catagories);
            var dialog = await _dialogService.Show<CustomerDialog>("Add customer", parameters).Result;

            if (dialog.Data != null)
            {
                CustomerDto customer = dialog.Data as CustomerDto;

                var response = await _httpClient.PostAsJsonAsync<CustomerDto>("api/Customers", customer);
                var putContent = await response.Content.ReadAsStringAsync();
                ResponseDto<CustomerDto>? authResponse = 
                    JsonSerializer.Deserialize<ResponseDto<CustomerDto>>(putContent, _options);

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

                    _snackbar.Add($"{addAdmin!.CompanyName} is added successfully.", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.Info;
                    });
                    await LoadAsync();
                }
            }
        }
        public async Task UpdateAsync(CustomerDto adminDto)
        {
            var parameters = new DialogParameters();
            parameters.Add("customer", adminDto);
            parameters.Add("catagories", Catagories);
            var dialog = await _dialogService.Show<CustomerDialog>("Edit customer", parameters).Result;

            if (dialog.Data != null)
            {
                CustomerDto admin = dialog.Data as CustomerDto;

                var response = await _httpClient.PutAsJsonAsync<CustomerDto>($"api/Customers/{admin.Id}", admin);
                var putContent = await response.Content.ReadAsStringAsync();
                ResponseDto<CustomerDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<CustomerDto>>(putContent, _options);

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

                    _snackbar.Add($"{addAdmin!.CompanyName} is updated successfully.", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.Info;
                    });
                    await LoadAsync();
                }
            }
        }
        public async Task DeleteAsync(CustomerDto employee)
        {
            var parameters = new DialogParameters();
            parameters.Add("ConfirmMessage", $"Are you shure you want to delete {employee.FullName} account?");
            var dialog = await _dialogService.Show<ConfirmDialog>("Confirm to delete", parameters).Result;

            if (dialog.Data != null)
            {
                var deleteResponse = await _httpClient.DeleteAsync($"api/Customers/{employee.Id}");
                var putContent = await deleteResponse.Content.ReadAsStringAsync();
                ResponseDto<CustomerDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<CustomerDto>>(putContent, _options);

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
