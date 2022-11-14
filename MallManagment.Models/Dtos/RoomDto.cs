using MallManagment.Models.Enums;
using MallManagment.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Dtos 
{
    public class RoomDto : BaseModel
    {
        public static implicit operator RoomDto(Room model)
        {
            return new RoomDto
            {
                Id = model.Id,
                Floor = model.Floor,
                Amount = model.Amount,
                RoomNumber = model.RoomNumber,
                RoomName = $"{model.Floor} : {model.RoomNumber}",
                Area = model.Area,
                Status = model.Status,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
            };
        }
        public static implicit operator Room(RoomDto model)
        {
            return new Room
            {
                Id = model.Id,
                Amount = model.Amount,
                Floor = model.Floor,
                RoomNumber = model.RoomNumber,
                Area = model.Area,
                Status = model.Status,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
            };
        }
        [StringLength(10)]
        [Required(ErrorMessage = "Floor is required")]
        public string? Floor { get; set; }
        public int RoomNumber { get; set; }
        [Required(ErrorMessage = "Area in m*m is required")]
        [Range(1, 9999, ErrorMessage = "Area should less than 9999m.m")]
        public float Area { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        [Range(1, 999999, ErrorMessage = "Amount should be from 1 to 1,000,000 birr")]
        public float Amount { get; set; }
        [Required]
        [Range(111, 114, ErrorMessage = "Status is required")]
        public RoomStatus Status { get; set; }

        // EXTENDED
        public string? RoomName { get; set; }
    }
}
