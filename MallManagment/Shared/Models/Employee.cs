﻿using MallManagment.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Shared.Models
{
    public class Employee: User
    {

        public int EmployeeNumber { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Occupation is required")]
        public string? Occupation { get; set; }

        public decimal Salary { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Range(111, 113, ErrorMessage = "Status is required")]
        public UserStatus Status { get; set; }

        public bool IsPermanent { get; set; }

        // NAVIGATIONS
        public virtual ICollection<BankReading> BankReadings { get; set; } = null!;
    }
}
