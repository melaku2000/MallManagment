using MallManagment.Shared.Enums;
using MallManagment.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Shared.Dtos
{
    public class BankDto : BaseModel
    {
        public static implicit operator BankDto(Bank model)
        {
            return new BankDto
            {
                Id = model.Id,
                Email = model.Email,
                AccountNumber = model.AccountNumber,
                BankName = model.BankName,
                BranchName = model.BranchName,
                ContactPerson = model.ContactPerson,
                ContactMobilePerson = model.ContactMobilePerson,
                OfficePhone = model.OfficePhone,
                OfficeAddress = model.OfficeAddress,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
            };
        }
        public static implicit operator Bank(BankDto model)
        {
            return new Bank
            {
                Id = model.Id,
                Email = model.Email,
                AccountNumber = model.AccountNumber,
                BankName = model.BankName,
                BranchName = model.BranchName,
                ContactPerson = model.ContactPerson,
                ContactMobilePerson = model.ContactMobilePerson,
                OfficePhone = model.OfficePhone,
                OfficeAddress = model.OfficeAddress,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate, 
            };
        }
        [StringLength(50)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bank name is required")]
        public string? BankName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Branch name is required")]
        public string? BranchName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Account number is required")]
        public string? AccountNumber { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Contact person is required")]
        public string? ContactPerson { get; set; }

        [StringLength(20)]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Contact person phone is required")]
        public string? ContactMobilePerson { get; set; }

        [StringLength(20)]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Office phone is required")]
        public string? OfficePhone { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Office adress is required")]
        public string? OfficeAddress { get; set; }
    }
}
