using MallManagment.Web.Components;
using MallManagment.Web.Pages.Admin;
using MallManagment.Web.Services;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Web.IViewModels
{
    public interface IRoomViewModel
    {
        public RoomDto? RoomDto { get; set; }
        public List<RoomDto> Rooms { get; set; }

        Task LoadAsync();
        Task CreateAsync();
        Task UpdateAsync(RoomDto room);
        Task DeleteAsync(RoomDto room);
    }
}
