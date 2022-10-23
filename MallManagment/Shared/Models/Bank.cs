using MallManagment.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Shared.Models
{
    public class Bank : BaseModel
    {
        [StringLength(50)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        
        [StringLength(50)]
        [Required(ErrorMessage = "Bank name is required")]
        public string? BankName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Branch name is required")]
        public string? BranchName { get; set; }
       
        [StringLength(50)]
        [Required(ErrorMessage = "Account number is required")]
        public string? AccountNumber { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Contact person is required")]
        public string? ContactPerson { get; set; }
        
        [StringLength(20)]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Contact person phone is required")]
        public string? ContactMobilePerson { get; set; }
        
        [StringLength(20)]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Office phone is required")]
        public string? OfficePhone { get; set; }
        
        [StringLength(20)]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Office adress is required")]
        public string? OfficeAddress { get; set; }

        //NAVIGATIONS
        //public virtual ICollection<RentalMessage> RentMessages { get; set; } = null!;
    }
}
