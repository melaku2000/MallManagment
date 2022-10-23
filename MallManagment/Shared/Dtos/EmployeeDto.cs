using MallManagment.Shared.Enums;
using MallManagment.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Shared.Dtos
{
    public class EmployeeDto : User
    {
        public static implicit operator EmployeeDto(Employee model)
        {
            return new EmployeeDto
            {
                Id = model.Id,
                FullName = model.FullName,
                EmployeeNumber = model.EmployeeNumber,
                Address = model.Address,
                IdentificationType = model.IdentificationType,
                IDNumber = model.IDNumber,
                MobilePhone = model.MobilePhone,
                Occupation = model.Occupation,
                Salary = model.Salary,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
                IsPermanent = model.IsPermanent,
                TinNumber = model.TinNumber,
                Status = model.Status
            };
        }
        public int EmployeeNumber { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Occupation is required")]
        public string? Occupation { get; set; }

        public decimal Salary { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Range(111, 113, ErrorMessage = "Status is required")]
        public UserStatus Status { get; set; }

        public bool IsPermanent { get; set; }
    }
}
