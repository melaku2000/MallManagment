using MallManagment.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Entities
{
    public class Room:BaseModel
    {
        public int RoomNumber { get; set; }
        [Required(ErrorMessage ="Area in m*m is required")]
        [Range(1,9999,ErrorMessage ="Area should less than 9999m.m")]
        public float Area { get; set; }
        [Required(ErrorMessage ="Amount is required")]
        [Range(1,999999,ErrorMessage = "Amount should be from 1 to 1,000,000 birr")]
        public float Amount { get; set; }
        [Required]
        [Range(111, 114, ErrorMessage = "Status is required")]
        public RoomStatus Status { get; set; }

        //NAVIGATIONS
        public virtual Customer Client { get; set; } = null!;
        public virtual ICollection<Rent> Rents { get; set; } = null!;
    }
}
