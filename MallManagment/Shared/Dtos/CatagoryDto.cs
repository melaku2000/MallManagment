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
    public class CatagoryDto : BaseModel
    {
        public static implicit operator CatagoryDto(BusinessCatagory model)
        {
            return new CatagoryDto
            {
                Id = model.Id,
                CatagoryName= model.CatagoryName,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
            };
        }
        [StringLength(50)]
        [Required(ErrorMessage = "Business catagory is required")]
        public string? CatagoryName { get; set; }
    }
}
