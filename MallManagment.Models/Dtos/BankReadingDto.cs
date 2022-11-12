using MallManagment.Models.Enums;
using MallManagment.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Dtos
{
    public class BankReadingDto : BaseModel
    {
        public static implicit operator BankReadingDto(BankReading model)
        {
            return new BankReadingDto
            {
                Id = model.Id,
                EmployeeId = model.EmployeeId,
                BankId = model.BankId,
                CurrentReading = model.CurrentReading,
                Note = model.Note,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
            };
        }
        [Required]
        public string? EmployeeId { get; set; }

        [Required]
        public string? BankId { get; set; }

        [Required(ErrorMessage = "Current account reading is required")]
        public decimal CurrentReading { get; set; }

        [StringLength(50)]
        public string? Note { get; set; }
    }
}
