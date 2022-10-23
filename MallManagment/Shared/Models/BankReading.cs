using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Shared.Models
{
    public class BankReading:BaseModel
    {
        [ForeignKey(nameof(Employee))]
        public string? EmployeeId { get; set; }
       
        [ForeignKey(nameof(Bank))]
        public string? BankId { get; set; }

        [Required(ErrorMessage = "Current account reading is required")]
        public decimal CurrentReading { get; set; }
        
        [StringLength(50)]
        public string? Note { get; set; }

        // NAVIGATION
        public virtual Employee Employee { get; set; } = null!;
        public virtual Bank Bank { get; set; } = null!;
    }
}
