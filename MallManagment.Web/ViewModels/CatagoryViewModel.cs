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
using MallManagment.Web.ViewModels;

namespace MallManagment.Web.IViewModels
{
    public class CatagoryViewModel : BaseViewModel, ICatagoryViewModel
    {
        public CatagoryDto? CatagoryDto { get; set; }
        public List<CatagoryDto> Catagories { get; set; } = new List<CatagoryDto>();
        public CatagoryViewModel(HttpClient httpClient, IDialogService dialogService, ISnackbar snackbar)
            : base(httpClient, dialogService, snackbar) { } 

        public async Task LoadAsync()
        {
            var adminResponse = await _httpClient.GetFromJsonAsync<ResponseDto<List<CatagoryDto>>>("api/Catagories/list");
            if (adminResponse!.Status == ResponseStatus.Success)
                Catagories = adminResponse.Model!;
        }
        public async Task CreateAsync()
        {
            var parameters = new DialogParameters();
            var adminDto = new CatagoryDto();

            parameters.Add("catagory", adminDto);
            var dialog = await _dialogService.Show<CatagoryDialog>("Add catagory", parameters).Result;

            if (dialog.Data != null)
            {
                CatagoryDto admin = dialog.Data as CatagoryDto;

                var response = await _httpClient.PostAsJsonAsync<CatagoryDto>("api/Catagories", admin);
                var putContent = await response.Content.ReadAsStringAsync();
                ResponseDto<CatagoryDto>? authResponse = 
                    JsonSerializer.Deserialize<ResponseDto<CatagoryDto>>(putContent, _options);

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

                    _snackbar.Add($"{addAdmin!.CatagoryName} is added successfully.", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.Info;
                    });
                    await LoadAsync();
                }
            }
        }
        public async Task UpdateAsync(CatagoryDto adminDto)
        {
            var parameters = new DialogParameters();
            parameters.Add("catagory", adminDto);
            var dialog = await _dialogService.Show<CatagoryDialog>("Edit catagory", parameters).Result;

            if (dialog.Data != null)
            {
                CatagoryDto admin = dialog.Data as CatagoryDto;

                var response = await _httpClient.PutAsJsonAsync<CatagoryDto>($"api/Catagories/{admin.Id}", admin);
                var putContent = await response.Content.ReadAsStringAsync();
                ResponseDto<CatagoryDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<CatagoryDto>>(putContent, _options);

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

                    _snackbar.Add($"{addAdmin!.CatagoryName} is updated successfully.", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.Info;
                    });
                    await LoadAsync();
                }
            }
        }
        public async Task DeleteAsync(CatagoryDto employee)
        {
            var parameters = new DialogParameters();
            parameters.Add("ConfirmMessage", $"Are you shure you want to delete {employee.CatagoryName} account?");
            var dialog = await _dialogService.Show<ConfirmDialog>("Confirm to delete", parameters).Result;

            if (dialog.Data != null)
            {
                var deleteResponse = await _httpClient.DeleteAsync($"api/Catagories/{employee.Id}");
                var putContent = await deleteResponse.Content.ReadAsStringAsync();
                ResponseDto<CatagoryDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<CatagoryDto>>(putContent, _options);

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
                    _snackbar.Add($"{employee.CatagoryName} is deleted successfully", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.DeleteForever;
                    });
                }
            }

        }
    }
}
