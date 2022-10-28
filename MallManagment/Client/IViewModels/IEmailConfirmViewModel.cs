using MallManagment.Shared.Dtos;

namespace MallManagment.Client.IViewModels
{
    public interface IEmailConfirmViewModel
    {
        public ConfirmPasswordDto? EmailConfirmDto { get; set; }
        public ConfirmDto ConfirmDto { get; set; }
        Task LoadAsync();
        Task SetPasswordAsync();
    }
}
