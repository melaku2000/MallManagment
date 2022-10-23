using MallManagment.Shared.Enums;
using MallManagment.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Shared.Dtos
{
    public class CustomerDto : User
    {
        public static implicit operator CustomerDto(Customer model)
        {
            var dto= new CustomerDto
            {
                Id = model.Id,
                Email = model.Email,
                FullName = model.FullName,
                IdentificationType = model.IdentificationType,
                IDNumber = model.IDNumber,
                TinNumber = model.TinNumber,
                MobilePhone = model.MobilePhone,

                CatagoryId = model.CatagoryId,
                CompanyName = model.CompanyName,
                CompanyTin = model.CompanyTin,
                OfficePhone = model.OfficePhone,
                Address = model.Address,
               
                CompanyUrl = model.CompanyUrl,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
            };
            if (model.Catagory != null) dto.CatagoryName = model.Catagory.CatagoryName;
            return dto;
        }
        [StringLength(50)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Catagory is required")]
        public string? CatagoryId { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [StringLength(50, ErrorMessage = "Company name should less than 50 character")]
        public string? CompanyName { get; set; }

        [Required(ErrorMessage = "Company TIN is required")]
        [StringLength(12, ErrorMessage = "Company TIN should less than 12 character")]
        public string? CompanyTin { get; set; }

        [Phone]
        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string? OfficePhone { get; set; }

        [StringLength(50)]
        public string? CompanyUrl { get; set; }
        // EXTENDED
        public string? CatagoryName { get; set; }
    }
}
