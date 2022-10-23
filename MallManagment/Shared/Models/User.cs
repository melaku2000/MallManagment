using MallManagment.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Shared.Models
{
    public class User : BaseModel
    {
        [Required]
        [Range(121, 126, ErrorMessage = "Identification card type is required")]
        public IdentificationType IdentificationType { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "ID number is required")]
        public string? IDNumber { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Full name is required")]
        public string? FullName { get; set; }

        [StringLength(20)]
        [Phone]
        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        public string? MobilePhone { get; set; }
       
        [StringLength(20, ErrorMessage = "Tin number is less than 20 characters")]
        [Required(ErrorMessage = "Tin number is required")]
        public string? TinNumber { get; set; }
        
        [StringLength(50)]
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }

       
        // NAVIGATION
        //public virtual ICollection<Client> Clients { get; set; } = null!; 
        //public virtual ICollection<Approval> Approvals { get; set; } = null!; 
    }
}
