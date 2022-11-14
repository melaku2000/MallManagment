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
    public class CatagoryDto : BaseModel
    {
        public static implicit operator CatagoryDto(BusinessCatagory model)
        {
            return new CatagoryDto
            {
                Id = model.Id,
                CatagoryName = model.CatagoryName,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
            };
        }
        public static implicit operator BusinessCatagory(CatagoryDto model)
        {
            return new BusinessCatagory
            {
                Id = model.Id,
                CatagoryName = model.CatagoryName,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
            };
        }
        [StringLength(50)]
        [Required(ErrorMessage = "Business catagory is required")]
        public string? CatagoryName { get; set; }
    }
}
