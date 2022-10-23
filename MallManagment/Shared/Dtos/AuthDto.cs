using MallManagment.Shared.Enums;
using MallManagment.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Shared.Dtos
{
    public class AuthDto
    {
        public static implicit operator AuthDto(Adminstrator user)
        {
            var dto= new AuthDto
            {
                Id = user.EmployeeId,
                Email = user.Email, LockCount = user.LockCount, LastLoginTime = user.LastLoginTime,
                 Role = user.Role, 
                EmailConfirmed = user.EmailConfirmed,
                PhoneConfirmed = user.PhoneConfirmed,  
            };
            if (user.Employee != null)
            {
                dto.FullName = user.Employee.FullName;
                dto.EmployeeNumber = user.Employee.EmployeeNumber;
            }
            return dto;
        }
        public string? Id { get; set; }
        public int EmployeeNumber { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
        public int LockCount { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLoginTime { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime TokenExpireTime { get; set; }
       
        public RoleType Role { get; set; }
    }
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
