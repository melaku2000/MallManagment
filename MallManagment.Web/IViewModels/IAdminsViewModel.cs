using MallManagment.Web.Services;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Web.IViewModels
{
    public interface IAdminsViewModel
    {
        public AdminstratorDto AdminDto { get; set; }
        public List<EmployeeDto> Employees { get; set; }
        public List<AdminstratorDto> Admins { get; set; }
        Task CreateAsync();
        Task UpdateAsync(AdminstratorDto adminDto);
        Task LoadAsync();
        Task DeleteAsync(AdminstratorDto dto);
    }
}
