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
    public class Message
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        [Range(141, 144, ErrorMessage = "Message type is required")]
        public MessageType MessageType { get; set; }
        
        [StringLength(50)]
        [Required(ErrorMessage = "Content is required")]
        public string? Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        //NAVIGATIONS
        public virtual ICollection<RentalMessage> RentMessages { get; set; } = null!;
    }
}
