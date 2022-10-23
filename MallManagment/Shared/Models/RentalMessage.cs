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
    public class RentalMessage
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        [ForeignKey(nameof(Rent))]
        public string? RentId { get; set; }
       
        [Required]
        [ForeignKey(nameof(Message))]
        public string? MessageId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        //NAVIGATIONS
        public virtual Rent Rent { get; set; } = null!;
        public virtual Message Message { get; set; } = null!;
    }
}
