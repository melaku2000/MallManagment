using MallManagment.Shared.Enums;
using MallManagment.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Shared.Dtos
{
    public class AdminstratorDto
    {
        public static implicit operator AdminstratorDto(Adminstrator model)
        {
            var dto= new AdminstratorDto
            {
                EmployeeId = model.EmployeeId,
                Email = model.Email,
                EmailConfirmed = model.EmailConfirmed,
                LastLoginTime = model.LastLoginTime,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
                LockCount = model.LockCount,
                PhoneConfirmed = model.PhoneConfirmed,
                Status = model.Status,
                Role = model.Role
            };
            if (model.Employee != null)
            {
                dto.FullName=model.Employee.FullName;
                dto.IDNumber=model.Employee.IDNumber;
            }
            return dto;
        }
        [Required]
        public string? EmployeeId { get; set; }

        [StringLength(50)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
        public int LockCount { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLoginTime { get; set; }

        [Range(101, 104, ErrorMessage = "Role is required")]
        public RoleType Role { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }

        [Range(111, 113, ErrorMessage = "Status is required")]
        public UserStatus Status { get; set; }

        // EXTENDED
        public string? IDNumber { get; set; }

        public string? FullName { get; set; }

    }
}
