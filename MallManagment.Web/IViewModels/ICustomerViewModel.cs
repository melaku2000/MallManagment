using MallManagment.Web.Components;
using MallManagment.Web.Pages.Admin;
using MallManagment.Web.Services;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Web.IViewModels
{
    public interface ICustomerViewModel
    {
        public CustomerDto? customerDto { get; set; }
        public List<CustomerDto> Customers { get; set; }
        Task CreateAsync();
        Task UpdateAsync(CustomerDto adminDto);
        Task LoadAsync();
        Task DeleteAsync(CustomerDto dto);
    }
}
