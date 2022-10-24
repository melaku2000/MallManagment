using MallManagment.Client.Services;
using MallManagment.Shared.Dtos;

namespace MallManagment.Client.ViewModels
{
    public class ProfileViewModel: IProfileViewModel
    {
        public Services.IFanaHttpService<EmployeeDto> _fanaHttpService { get; }
        public ProfileViewModel()
        {

        }
        public ProfileViewModel(IFanaHttpService<EmployeeDto> fanaHttpService)
        {
            _fanaHttpService = fanaHttpService;
        }
        public EmployeeDto? Employee { get; set; }
        public string? Message { get; set; }

        public async Task GetProfile()
        {
            Message = "On Get button cliked";
            Employee = await _fanaHttpService.GetById("api/employees", "melaku1234");
        }
        public async Task OnSubmit()
        {
            Message = "On Submit cliked";
            var result = await _fanaHttpService.Update("api/employees/melaku1234", Employee);
        }
    }
}
