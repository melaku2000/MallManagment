using MallManagment.Shared.Dtos;

namespace MallManagment.Client.IViewModels
{
    public interface IProfileViewModel
    {
        public EmployeeDto? Employee { get; set; }
        public string? Message { get; set; }

        Task GetProfile();
        Task OnSubmit();
    }
}
