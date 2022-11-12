using MallManagment.Models.Dtos;

namespace MallManagment.Web.IViewModels
{
    public interface IProfileViewModel
    {
        public EmployeeDto? Employee { get; set; }
        public string? Message { get; set; }

        Task GetProfile();
        Task OnSubmit();
    }
}
