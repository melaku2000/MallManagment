using MallManagment.Web.Components;
using MallManagment.Web.Pages.Admin;
using MallManagment.Web.Services;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Web.IViewModels
{
    public interface IBalanceViewModel
    {
        public string? EmployeeId { get; set; }
        public BalanceDto? BalanceDto { get; set; }

        public BankDto? BankDto { get; set; }
        public List<BankDto> Banks { get; set; }
        public DateTime? CurrentDate { get; set; }
        public List<BalanceDto> Balances { get; set; }

        Task LoadAsync();
        Task LoadBalanceAsync(); 
        Task CreateAsync(BankDto bank);
        Task UpdateAsync(BalanceDto balance);
        Task DeleteAsync(BalanceDto balance);
    }
}
