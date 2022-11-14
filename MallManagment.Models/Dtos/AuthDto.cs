using MallManagment.Models.Enums;
using MallManagment.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Dtos
{
    public class AuthDto
    {
        public static implicit operator AuthDto(Adminstrator user)
        {
            var dto = new AuthDto
            {
                EmployeeId = user.EmployeeId,
                Email = user.Email,
                LockCount = user.LockCount,
                LastLoginTime = user.LastLoginTime,
                StringRole = user.Role.ToString(),
                EmailConfirmed = user.EmailConfirmed,
                PhoneConfirmed = user.PhoneConfirmed,
                RefreshToken = user.RefreshToken,
                TokenExpireTime = user.TokenExpireTime
            };
            if (user.Employee != null)
            {
                dto.FullName = user.Employee.FullName;
                dto.EmployeeNumber = user.Employee.EmployeeNumber;
            }
            return dto;
        }
        public string? EmployeeId { get; set; }
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
       
        public string? StringRole { get; set; }
    }
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
    public class ConfirmDto
    {
        [Required]
        public string? EmployeeId { get; set; }
        [Required(ErrorMessage = "Token is required.")]
        public string? Token { get; set; }
    }
    public class ConfirmPasswordDto
    {
        public static implicit operator ConfirmPasswordDto(UserToken userToken)
        {
            return new ConfirmPasswordDto { AdminId = userToken.AdminId, Token = userToken.Token };
        }
        [Required]
        public string? AdminId { get; set; }
        [Required(ErrorMessage = "Token is required.")]
        public string? Token { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        [Compare(nameof(Password),ErrorMessage = "Password and Confirm password do not match.")]
        public string? ConfirmPassword { get; set; }
    }

    public class RequestRefreshTokenDto
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
