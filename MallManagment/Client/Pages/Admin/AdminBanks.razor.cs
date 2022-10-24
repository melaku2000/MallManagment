using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Reflection;
using System.Security.Claims;
using MallManagment.Shared.Dtos;
using MallManagment.Client.Services;
using MallManagment.Shared.Enums;
using MallManagment.Shared.Models;
using MallManagment.Client.Components;

namespace MallManagment.Client.Pages.Admin
{
    //public partial class AdminBanks
    //{
    //    private BankDto? BankDto { get; set; }
    //    private List<BankDto> banks { get; set; } = new List<BankDto>();
      
    //    [Inject]
    //    public IFanaHttpService<BankDto> bankRepo { get; set; }
    //    protected override async Task OnInitializedAsync()
    //    {
    //        await LoadInitial();
    //    }
    //    async Task LoadInitial()
    //    {
    //        banks = await bankRepo.GetLists("api/banks/list");
    //    }
    //    private async Task Create()
    //    {
    //        var parameters = new DialogParameters();
    //        var bankDto = new BankDto()
    //        {
               
    //        };
    //        parameters.Add("bank", bankDto);
    //        var dialog = await _dialogService.Show<BankDialog>("Add bank", parameters).Result;

    //        if (dialog.Data != null)
    //        {
    //            BankDto bank = dialog.Data as BankDto;

    //            var response = await bankRepo.Create("api/banks", bank);
    //            if (response.Status == ResponseStatus.Success)
    //                await LoadInitial();
    //        }
    //    }
    //    private async Task Update(BankDto bank)
    //    {
    //        var parameters = new DialogParameters();
    //        parameters.Add("bank", bank);
    //        var dialog = await _dialogService.Show<BankDialog>("Edit bank", parameters).Result;

    //        if (dialog.Data != null)
    //        {
    //            BankDto bankDto = dialog.Data as BankDto;

    //            var response = await bankRepo.Update($"api/banks/{bankDto.Id}", bankDto);
    //            if (response.Status == ResponseStatus.Success)
    //                await LoadInitial();
    //        }
    //    }
    //    async Task DeleteAsync(BankDto bank)
    //    {
    //        var parameters = new DialogParameters();
    //        parameters.Add("ConfirmMessage", $"Are you shure you want to delete {bank.BankName} account?");
    //        var dialog = await _dialogService.Show<ConfirmDialog>("Confirm to delete", parameters).Result;

    //        if (dialog.Data != null)
    //        {
    //            var response = await bankRepo.Delete("api/banks", bank.Id);
    //            if (response.Status == ResponseStatus.Success)
    //                await LoadInitial();
    //        }

    //    }
    //}
}
