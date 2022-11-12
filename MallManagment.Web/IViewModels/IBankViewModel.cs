using MallManagment.Web.Services;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Web.IViewModels
{
    public interface IBankViewModel
    {
        public BankDto BankDto { get; set; }
        public List<BankDto> Banks { get; set; }

        Task LoadAsync();
        Task CreateAsync();
        Task UpdateAsync(BankDto bank);
        Task DeleteAsync(BankDto bank);
    }
}
