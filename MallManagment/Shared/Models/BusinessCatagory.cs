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
    public class BusinessCatagory :BaseModel
    {
        
        [StringLength(50)]
        [Required(ErrorMessage = "Business catagory is required")]
        public string? CatagoryName { get; set; }

        //NAVIGATIONS
        public virtual ICollection<Customer> Clients { get; set; } = null!;
    }
}
