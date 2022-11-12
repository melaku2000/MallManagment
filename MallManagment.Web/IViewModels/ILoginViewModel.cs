using MallManagment.Models.Dtos;

namespace MallManagment.Web.IViewModels
{
    public interface ILoginViewModel
    {
        public LoginDto loginDto { get; set; }
        public bool ShowAuthError { get; set; }
        public string? Error { get; set; }
        Task OnSubmitAsync();
        void CloseError();
    }
}
