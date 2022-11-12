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
    public class Approval:BaseModel
    {
        [ForeignKey(nameof(Payment))]
        public string? PaymentId { get; set; }
       
        [ForeignKey(nameof(Admin))]
        public string? AdminId { get; set; }

        [StringLength(200, ErrorMessage = "Note should be less than 200 character")]
        public string? Note { get; set; }

        [Required]
        [Range(131, 133, ErrorMessage = "Status is required")]
        public RowStatus Status { get; set; }
        //NAVIGATIONS
        public virtual Payment Payment { get; set; } = null!;
        public virtual User Admin { get; set; } = null!;
    }
}
