using MallManagment.Models.Dtos;

namespace MallManagment.Web.IViewModels
{
    public interface IEmployeeDetailViewModel
    {
        public EmployeeDto EmployeeDto { get; set; }
        public bool ShowAuthError { get; set; }
        public string? Error { get; set; }
        Task OnSubmitAsync();
        Task LoadAsync(string empoyeeid);
        void CloseError();
    }
}
