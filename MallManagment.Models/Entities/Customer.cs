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
    public class Customer : User
    {
        [StringLength(50)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required]
        [ForeignKey(nameof(Catagory))]
        public string? CatagoryId { get; set; }

        [Required(ErrorMessage ="Company name is required")]
        [StringLength(50,ErrorMessage ="Company name should less than 50 character")]
        public string? CompanyName { get; set; }
       
        [Required(ErrorMessage = "Company TIN is required")]
        [StringLength(12, ErrorMessage = "Company TIN should less than 12 character")]
        public string? CompanyTin { get; set; }
        
        [Phone]
        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string? OfficePhone { get; set; }
        
        [StringLength(50)]
        public string? CompanyUrl { get; set; }

        //NAVIGATIONS
        public virtual BusinessCatagory Catagory { get; set; } = null!;
    }
}
