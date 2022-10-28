﻿using MallManagment.Client.Components;
using MallManagment.Client.Pages.Admin;
using MallManagment.Client.Services;
using MallManagment.Shared.Dtos;
using MallManagment.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManagment.Client.IViewModels
{
    public interface IEmployeeViewModel
    {
        public EmployeeDto EmployeeDto { get; set; }
        public List<EmployeeDto> Employees { get; set; }

        Task LoadAsync();
        Task DeleteAsync(EmployeeDto dto);
    }
}
