using MallManagment.Client.IViewModels;
using MallManagment.Shared.Dtos;
using MallManagment.Shared.Enums;
using MallManagment.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MallManagment.Client.ViewModels
{
    public class EmployeeDetailViewModel : IEmployeeDetailViewModel
    {
        private HttpClient _httpClient;
        private readonly NavigationManager _navManager;
        private readonly JsonSerializerOptions _options;

        public EmployeeDetailViewModel()
        {

        }
        public EmployeeDetailViewModel(HttpClient httpClient, NavigationManager navManager)
        {
            _httpClient = httpClient;
            this._navManager = navManager;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public bool ShowAuthError { get; set; }
        public EmployeeDto? EmployeeDto { get; set; }
        public string? Error { get; set; }

        public void CloseError()
        {
            ShowAuthError=false;
        }

        public async Task LoadAsync(string id) 
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseDto<EmployeeDto>>($"api/Employees/{id}");
                if (response!.Status == ResponseStatus.Success)
                {
                    EmployeeDto = response.Model!;
                }
            }
            else
                EmployeeDto = new EmployeeDto { Status = UserStatus.Active };
        }
        public async Task OnSubmitAsync()
        {
            HttpResponseMessage responseMessage;
            if (EmployeeDto!.EmployeeNumber == 0)
            {
                responseMessage = await _httpClient.PostAsJsonAsync<EmployeeDto>("api/Employees", EmployeeDto);
            }
            else
            {
                responseMessage = await _httpClient.PutAsJsonAsync<EmployeeDto>($"api/Employees/{EmployeeDto.Id}", EmployeeDto);
            }
           
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            ResponseDto<EmployeeDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<EmployeeDto>>(responseContent, _options);

            if (authResponse.Status != ResponseStatus.Success)
            {
                Console.WriteLine(responseContent);
                ShowAuthError = true;
                Error = authResponse.Message;
            }
            else
                _navManager.NavigateTo("/admin/employees");
        }
    }
}
