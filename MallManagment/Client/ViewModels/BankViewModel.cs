using MallManagment.Client.Components;
using MallManagment.Client.Pages.Admin;
using MallManagment.Client.Services;
using MallManagment.Shared.Dtos;
using MallManagment.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Client.ViewModels
{
    public class BankViewModel: IBankViewModel
    {
        public BankDto? BankDto { get; set; }
        public List<BankDto> Banks { get; set; } = new List<BankDto>();
        public IFanaHttpService<BankDto> _bankRepo { get; }
        public IDialogService _dialogService { get; }

        public BankViewModel()
        {

        }
        public BankViewModel(IFanaHttpService<BankDto> bankRepo, IDialogService dialogService)
        {
            this._bankRepo = bankRepo;
            _dialogService = dialogService;
        }
       
        public async Task LoadAsync()
        {
            Banks = await _bankRepo.GetLists("api/banks/list");
        }
        public async Task CreateAsync()
        {
            var parameters = new DialogParameters();
            var bankDto = new BankDto()
            {

            };
            parameters.Add("bank", bankDto);
            var dialog = await _dialogService.Show<BankDialog>("Add bank", parameters).Result;

            if (dialog.Data != null)
            {
                BankDto bank = dialog.Data as BankDto;

                var response = await _bankRepo.Create("api/banks", bank);
                if (response.Status == ResponseStatus.Success)
                    await LoadAsync();
            }
        }
        public async Task UpdateAsync(BankDto bank)
        {
            var parameters = new DialogParameters();
            parameters.Add("bank", bank);
            var dialog = await _dialogService.Show<BankDialog>("Edit bank", parameters).Result;

            if (dialog.Data != null)
            {
                BankDto bankDto = dialog.Data as BankDto;

                var response = await _bankRepo.Update($"api/banks/{bankDto.Id}", bankDto);
                if (response.Status == ResponseStatus.Success)
                    await LoadAsync();
            }
        }
        public async Task DeleteAsync(BankDto bank)
        {
            var parameters = new DialogParameters();
            parameters.Add("ConfirmMessage", $"Are you shure you want to delete {bank.BankName} account?");
            var dialog = await _dialogService.Show<ConfirmDialog>("Confirm to delete", parameters).Result;

            if (dialog.Data != null)
            {
                var response = await _bankRepo.Delete("api/banks", bank.Id);
                if (response.Status == ResponseStatus.Success)
                    await LoadAsync();
            }

        }
    }
}
