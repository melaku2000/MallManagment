using MudBlazor;
using System.Text.Json;

namespace MallManagment.Client.ViewModels
{
    public class BaseViewModel
    {
        public HttpClient _httpClient { get; }
        public readonly JsonSerializerOptions _options;
        public IDialogService _dialogService { get; }
        public ISnackbar _snackbar { get; }

        public BaseViewModel(HttpClient httpClient, IDialogService dialogService, ISnackbar snackbar)
        {
            this._httpClient = httpClient;
            _dialogService = dialogService;
            _snackbar = snackbar;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
    }
}
