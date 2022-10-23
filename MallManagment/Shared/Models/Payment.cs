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
    public class Payment:BaseModel
    {
        [ForeignKey(nameof(Rent))]
        public string? RentId { get; set; }

        [Range(1, 999999, ErrorMessage = "Amount should be from 1 to 1,000,000 birr")]
        public float Amount { get; set; }

        [Required(ErrorMessage ="Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage ="End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(131, 133, ErrorMessage = "Status is required")]
        public RowStatus Status { get; set; }
        public bool SlipAttached { get; set; }
        //NAVIGATIONS
        public virtual Rent Rent { get; set; } = null!;
        public virtual ICollection<Approval> Approvals { get; set; } = null!;

    }
}
