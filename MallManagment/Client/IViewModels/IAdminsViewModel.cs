using MallManagment.Client.Components;
using MallManagment.Client.Pages.Admin;
using MallManagment.Client.Services;
using MallManagment.Shared.Dtos;
using MallManagment.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Client.IViewModels
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
