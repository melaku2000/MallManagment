using MallManagment.Client.Components;
using MallManagment.Client.Pages.Admin;
using MallManagment.Client.Services;
using MallManagment.Shared.Dtos;
using MallManagment.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Client.IViewModels
{
    public interface IIdentificationViewModel
    {
        public string? UserId { get; set; }
        public FileData? ImageData { get; set; } 
        public string? FileUrl { get; set; }
        Task LoadAsync();
    }
}
