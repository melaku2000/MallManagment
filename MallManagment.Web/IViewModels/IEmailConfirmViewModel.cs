using MallManagment.Models.Dtos;

namespace MallManagment.Web.IViewModels
{
    public interface IEmailConfirmViewModel
    {
        public ConfirmPasswordDto? EmailConfirmDto { get; set; }
        public ConfirmDto ConfirmDto { get; set; }
        Task LoadAsync();
        Task SetPasswordAsync();
    }
}
