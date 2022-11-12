using MallManagment.Web.Services;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Web.IViewModels
{
    public interface IIdentificationViewModel
    {
        public string? UserId { get; set; }
        public FileData? ImageData { get; set; } 
        public string? FileUrl { get; set; }
        Task LoadAsync();
    }
}
