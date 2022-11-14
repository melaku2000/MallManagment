using Blazored.LocalStorage;
using MallManagment.Models.Dtos;
using MallManagment.Web;
using MallManagment.Web.IViewModels;
using MallManagment.Web.Providers;
using MallManagment.Web.Services;
using MallManagment.Web.ViewModels;
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

//builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7245/")});

builder.Services.AddMudServices(c => { c.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight; });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<ITokenManagerService, TokenManagerService>();
builder.Services.AddScoped<IBankViewModel, BankViewModel>();

builder.Services.AddHttpClient<ILoginViewModel, LoginViewModel>("MallMgmt", cliient => cliient.BaseAddress = new Uri("https://localhost:7245/"));



builder.Services.AddScoped<IFanaHttpService<BankDto>, FanaHttpService<BankDto>>();
builder.Services.AddScoped<IFanaHttpService<EmployeeDto>, FanaHttpService<EmployeeDto>>();
builder.Services.AddScoped<IProfileViewModel, ProfileViewModel>();

builder.Services.AddHttpClient<IEmployeeViewModel, EmployeeViewModel>("MallMgmt", cliient => cliient.BaseAddress = new Uri("https://localhost:7245/"));
builder.Services.AddHttpClient<IEditEmployeeViewModel, EditEmployeeViewModel>("MallMgmt", cliient => cliient.BaseAddress = new Uri("https://localhost:7245/"));
builder.Services.AddHttpClient<IEmployeeDetailViewModel, EmployeeDetailViewModel>("MallMgmt", cliient => cliient.BaseAddress = new Uri("https://localhost:7245/"));

builder.Services.AddHttpClient<IAdminsViewModel, AdminsViewModel>("MallMgmt", cliient => cliient.BaseAddress = new Uri("https://localhost:7245/"));

builder.Services.AddHttpClient<ICustomerViewModel, CustomerViewModel>("MallMgmt", cliient => cliient.BaseAddress = new Uri("https://localhost:7245/"));
builder.Services.AddHttpClient<ICatagoryViewModel, CatagoryViewModel>("MallMgmt", cliient => cliient.BaseAddress = new Uri("https://localhost:7245/"));
builder.Services.AddHttpClient<IBalanceViewModel, BalanceViewModel>("MallMgmt", cliient => cliient.BaseAddress = new Uri("https://localhost:7245/"));

builder.Services.AddHttpClient<IRoomViewModel, RoomViewModel>("MallMgmt", cliient => cliient.BaseAddress = new Uri("https://localhost:7245/"));

await builder.Build().RunAsync();
