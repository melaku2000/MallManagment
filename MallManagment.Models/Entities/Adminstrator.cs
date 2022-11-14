using MallManagment.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Entities
{
    public class Adminstrator
    {
        [Key]
        [ForeignKey(nameof(Employee))]
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
        // FOR REFRESH TOKEN
        public string? RefreshToken { get; set; }
        public string? IpAddress { get; set; }
        public DateTime TokenExpireTime { get; set; }
        // NAVIGATION
        public virtual Employee Employee { get; set; } = null!;
        public virtual ICollection<UserToken> Tokens { get; set; } = null!;
    }
}
