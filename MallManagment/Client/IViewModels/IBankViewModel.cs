using MallManagment.Client.Components;
using MallManagment.Client.Pages.Admin;
using MallManagment.Client.Services;
using MallManagment.Shared.Dtos;
using MallManagment.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Client.IViewModels
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
