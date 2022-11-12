using MallManagment.Web.Services;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Web.IViewModels
{
    public interface IEmployeeViewModel
    {
        public EmployeeDto EmployeeDto { get; set; }
        public List<EmployeeDto> Employees { get; set; }

        Task LoadAsync();
        Task DeleteAsync(EmployeeDto dto);
    }
}
