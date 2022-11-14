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
    public class BalanceDto : BaseModel
    {
        public static implicit operator BalanceDto(Balance model)
        {
            var dto= new BalanceDto
            {
                Id = model.Id,
                EmployeeId = model.EmployeeId,
                BankId = model.BankId,
                AccountBalance = model.AccountBalance,
                Note = model.Note,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
            };
            if(model.Employee!=null) dto.FullName = model.Employee.FullName;
            if(model.Bank!=null) dto.BankName = model.Bank.BankName;
            return dto;
        }
        public static implicit operator Balance(BalanceDto model)
        {
            return new Balance
            {
                Id = model.Id,
                EmployeeId = model.EmployeeId,
                BankId = model.BankId,
                AccountBalance = model.AccountBalance,
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
        public decimal AccountBalance { get; set; }

        [StringLength(50)]
        public string? Note { get; set; }

        // EXTENDED
        public string? FullName { get; set; }
        public string? BankName { get; set; }

    }
    public class BalanceRead
    {
        public string? EmployeeId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
