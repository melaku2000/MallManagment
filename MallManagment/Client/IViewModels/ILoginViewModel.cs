using MallManagment.Shared.Dtos;

namespace MallManagment.Client.IViewModels
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
