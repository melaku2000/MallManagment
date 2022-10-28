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
    public class IdentificationViewModel : BaseViewModel, IIdentificationViewModel
    {
        public string? UserId { get; set; }
        public FileData? ImageData { get; set; }
        public string? FileUrl { get; set; }
        public IdentificationViewModel(HttpClient httpClient, IDialogService dialogService, ISnackbar snackbar)
            : base(httpClient, dialogService, snackbar) { } 

        public async Task LoadAsync()
        {
            var response = await _httpClient.GetAsync($"api/files/getID/{UserId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            ImageData = JsonSerializer.Deserialize<FileData>(content, _options);
        }
    }
}
