using MallManagment.Shared.Dtos;

namespace MallManagment.Client.IViewModels
{
    public interface IEditEmployeeViewModel
    {
        public EmployeeDto EmployeeDto { get; set; }
        public bool ShowAuthError { get; set; }
        public string? Error { get; set; }
        Task OnSubmitAsync();
        Task LoadAsync(string empoyeeid);
        void CloseError();
    }
}
