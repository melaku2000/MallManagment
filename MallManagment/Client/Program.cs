using MallManagment.Client;
using MallManagment.Client.IViewModels;
using MallManagment.Client.Providers;
using MallManagment.Client.Services;
using MallManagment.Client.ViewModels;
using MallManagment.Shared.Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// FOR AUTORIZATION
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices(c => { c.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight; });

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddScoped<IFanaHttpService<BankDto>, FanaHttpService<BankDto>>();
builder.Services.AddScoped<IFanaHttpService<EmployeeDto>, FanaHttpService<EmployeeDto>>();
builder.Services.AddScoped<IProfileViewModel,ProfileViewModel>();
builder.Services.AddScoped<IBankViewModel,BankViewModel>();
builder.Services.AddHttpClient<ILoginViewModel, LoginViewModel>("MallMgmt",cliient=>cliient.BaseAddress= new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient<IEmployeeViewModel, EmployeeViewModel>("MallMgmt",cliient=>cliient.BaseAddress= new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient<IEditEmployeeViewModel, EditEmployeeViewModel>("MallMgmt",cliient=>cliient.BaseAddress= new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient<IEmployeeDetailViewModel, EmployeeDetailViewModel>("MallMgmt",cliient=>cliient.BaseAddress= new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddHttpClient<IAdminsViewModel, AdminsViewModel>("MallMgmt",cliient=>cliient.BaseAddress= new Uri(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();
 