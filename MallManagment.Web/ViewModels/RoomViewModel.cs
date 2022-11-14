using MallManagment.Web.Components;
using MallManagment.Web.IViewModels;
using MallManagment.Web.Pages.Admin;
using MallManagment.Web.Pages.Employee;
using MallManagment.Web.Services;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using MallManagment.Web.ViewModels;

namespace MallManagment.Web.IViewModels
{
    public class RoomViewModel: BaseViewModel, IRoomViewModel
    {
        public RoomDto? RoomDto { get; set; }
        public List<RoomDto> Rooms { get; set; } = new List<RoomDto>();

        public RoomViewModel(HttpClient httpClient, IDialogService dialogService, ISnackbar snackbar)
            : base(httpClient, dialogService, snackbar) { }

        public async Task LoadAsync()
        {
            var bankResponse = await _httpClient.GetFromJsonAsync<ResponseDto<List<RoomDto>>>("api/rooms/list");
            if (bankResponse!.Status == ResponseStatus.Success)
                Rooms = bankResponse.Model!;
        }
        public async Task CreateAsync()
        {
            var parameters = new DialogParameters();
            var RoomDto = new RoomDto()
            {

            };
            parameters.Add("room", RoomDto);
            var dialog = await _dialogService.Show<RoomDialog>("Add room", parameters).Result;

            if (dialog.Data != null)
            {
                RoomDto room = dialog.Data as RoomDto;

                var response = await _httpClient.PostAsJsonAsync<RoomDto>("api/rooms", room);
                var putContent = await response.Content.ReadAsStringAsync();
                ResponseDto<RoomDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<RoomDto>>(putContent, _options);

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

                    _snackbar.Add($"{addAdmin!.RoomName} is added successfully.", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.Info;
                    });
                    await LoadAsync();
                }
            }
        }
        public async Task UpdateAsync(RoomDto room)
        {
            var parameters = new DialogParameters();
            parameters.Add("room", room);
            var dialog = await _dialogService.Show<RoomDialog>("Edit room", parameters).Result;

            if (dialog.Data != null)
            {
                RoomDto roomDto = dialog.Data as RoomDto;

                var response = await _httpClient.PutAsJsonAsync<RoomDto>($"api/rooms/{roomDto.Id}", roomDto);
                var putContent = await response.Content.ReadAsStringAsync();
                ResponseDto<RoomDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<RoomDto>>(putContent, _options);

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

                    _snackbar.Add($"{addAdmin!.RoomName} is updated.", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.Info;
                    });
                    await LoadAsync();
                }
            }
        }
        public async Task DeleteAsync(RoomDto room)
        {
            var parameters = new DialogParameters();
            parameters.Add("ConfirmMessage", $"Are you shure you want to delete {room.RoomName} account?");
            var dialog = await _dialogService.Show<ConfirmDialog>("Confirm to delete", parameters).Result;

            if (dialog.Data != null)
            {
                var deleteResponse = await _httpClient.DeleteAsync($"api/rooms/{room.Id}");
                var putContent = await deleteResponse.Content.ReadAsStringAsync();
                ResponseDto<RoomDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<RoomDto>>(putContent, _options);

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
                    _snackbar.Add($"{room.RoomName} is deleted successfully", Severity.Success, config =>
                    {
                        config.Icon = Icons.Filled.DeleteForever;
                    });
                }
            }

        }
    }
}
