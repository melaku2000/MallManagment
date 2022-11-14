using MallManagment.Web.Components;
using MallManagment.Web.IViewModels;
using MallManagment.Web.Pages.Admin;
using MallManagment.Web.Pages.Employee;
using MallManagment.Web.Services;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using MallManagment.Models.Extenssions;
using MallManagment.Models.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;
using MallManagment.Web.ViewModels;

namespace MallManagment.Web.IViewModels
{
    public class BalanceViewModel : BaseViewModel, IBalanceViewModel
    {
        public string? EmployeeId { get; set; }
        public BankDto? BankDto { get; set; }
        public BalanceDto? BalanceDto { get; set; }
        public List<BankDto> Banks { get; set; } = new List<BankDto>();
        public List<BalanceDto> Balances { get; set; } = new List<BalanceDto>();
        public DateTime? CurrentDate { get; set; }

        public BalanceViewModel(HttpClient httpClient, IDialogService dialogService, ISnackbar snackbar)
            : base(httpClient, dialogService, snackbar) 
        {
            CurrentDate = DateTime.UtcNow;
        }

        public async Task LoadAsync()
        {

            var bankResponse     = await _httpClient.GetFromJsonAsync<ResponseDto<List<BankDto>>>("api/banks/list");
            if (bankResponse?.Status == ResponseStatus.Success)
                Banks = bankResponse.Model!;
            
        }
        public async Task LoadBalanceAsync()
        {

            BalanceRead balanceRead = new BalanceRead { EmployeeId = EmployeeId, CreatedDate = (DateTime)CurrentDate! };

            var response = await _httpClient.PostAsJsonAsync<BalanceRead>("api/Balances/list", balanceRead);
            var putContent = await response.Content.ReadAsStringAsync();
            ResponseDto<List<BalanceDto>>? authResponse = JsonSerializer.Deserialize<ResponseDto<List<BalanceDto>>>(putContent, _options);

            if (authResponse!.Status == ResponseStatus.Success)
            {
                Balances = authResponse.Model!;
            }
        }
        public async Task CreateAsync(BankDto bank)
        {
            var parameters = new DialogParameters();
            var balanceDto = new BalanceDto()
            {
                BankId= bank.Id,BankName=bank.BankName,EmployeeId=EmployeeId
            };
            parameters.Add("balance", balanceDto);
            var dialog = await _dialogService.Show<BalanceDialog>("Add balance", parameters).Result;

            if (dialog.Data != null)
            {
                BalanceDto balance = dialog.Data as BalanceDto;

                var response = await _httpClient.PostAsJsonAsync<BalanceDto>("api/Balances", balance);
                var putContent = await response.Content.ReadAsStringAsync();
                ResponseDto<BalanceDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<BalanceDto>>(putContent, _options);

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

                    _snackbar.Add($"{addAdmin!.BankName} is added successfully.", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.Info;
                    });
                    await LoadAsync();
                }
            }
        }
        public async Task UpdateAsync(BalanceDto balance)
        {
            var parameters = new DialogParameters();
            balance.EmployeeId = EmployeeId;
            parameters.Add("balance", balance);
            var dialog = await _dialogService.Show<BalanceDialog>("Edit bank", parameters).Result;

            if (dialog.Data != null)
            {
                BalanceDto bankDto = dialog.Data as BalanceDto;

                var response = await _httpClient.PutAsJsonAsync<BalanceDto>($"api/Balances/{bankDto.Id}", bankDto);
                var putContent = await response.Content.ReadAsStringAsync();
                ResponseDto<BalanceDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<BalanceDto>>(putContent, _options);

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

                    _snackbar.Add($"{addAdmin!.BankName} is updated.", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.Info;
                    });
                    await LoadAsync();
                }
            }
        }
        public async Task DeleteAsync(BalanceDto balance)
        {
            var parameters = new DialogParameters();
            parameters.Add("ConfirmMessage", $"Are you shure you want to delete {balance.BankName} account?");
            var dialog = await _dialogService.Show<ConfirmDialog>("Confirm to delete", parameters).Result;

            if (dialog.Data != null)
            {
                var deleteResponse = await _httpClient.DeleteAsync($"api/Balances/{balance.Id}");
                var putContent = await deleteResponse.Content.ReadAsStringAsync();
                ResponseDto<BalanceDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<BalanceDto>>(putContent, _options);

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
                    _snackbar.Add($"{balance.BankName} is deleted successfully", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.DeleteForever;
                    });
                }
            }

        }
    }
}
