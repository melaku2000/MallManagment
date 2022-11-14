using MallManagment.Web.Components;
using MallManagment.Web.Pages.Admin;
using MallManagment.Web.Services;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Web.IViewModels
{
    public interface ICatagoryViewModel 
    {
        public CatagoryDto? CatagoryDto { get; set; }
        public List<CatagoryDto> Catagories { get; set; }
        Task CreateAsync();
        Task UpdateAsync(CatagoryDto adminDto);
        Task LoadAsync();
        Task DeleteAsync(CatagoryDto dto);
    }
}
