using MallManagment.Client;
using MallManagment.Client.Services;
using MallManagment.Client.ViewModels;
using MallManagment.Shared.Dtos;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices(c => { c.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight; });

builder.Services.AddScoped<IFanaHttpService<BankDto>, FanaHttpService<BankDto>>();
builder.Services.AddScoped<IFanaHttpService<EmployeeDto>, FanaHttpService<EmployeeDto>>();
builder.Services.AddScoped<IProfileViewModel,ProfileViewModel>();
builder.Services.AddScoped<IBankViewModel,BankViewModel>();

await builder.Build().RunAsync();
