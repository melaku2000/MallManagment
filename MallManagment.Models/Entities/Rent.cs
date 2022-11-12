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
    public class Rent:BaseModel
    {
        [ForeignKey(nameof(Client))]
        public string? ClientId { get; set; }
        [ForeignKey(nameof(Room))]
        public string? RoomId { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        [Range(1, 999999, ErrorMessage = "Amount should be from 1 to 1,000,000 birr")]
        public float MonthlyAmount { get; set; }

        [Required]
        [Range(121, 124, ErrorMessage = "Status is required")]
        public PaymentType PaymentType { get; set; }

        [Required(ErrorMessage ="agremment start date is required")]
        [DataType(DataType.Date)]
        public DateTime AgremmentStartDate { get; set; }

        [Required(ErrorMessage ="agremment end date is required")]
        [DataType(DataType.Date)]
        public DateTime AgremmentEndDate { get; set; }

        [Required]
        [Range(131, 133, ErrorMessage = "Status is required")]
        public RoomStatus Status { get; set; }
        //NAVIGATIONS
        public virtual Room Room { get; set; } = null!;
        public virtual Customer Client { get; set; } = null!;
        public virtual ICollection<RentalMessage> RentMessages { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; } = null!;
    }
}
