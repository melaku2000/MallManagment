using MallManagment.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallManagment.Api.Handlers;
using MallManagment.Models.Enums;

namespace MallManagment.Api.Data
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            var current = DateTime.UtcNow;

            builder.HasData(new Employee
            {
                Id = "melaku1234",
                FullName = "Melaku Michael",
                MobilePhone = "0911641927",
                CreatedDate = current,
                ModifyDate = current,
                IdentificationType = IdentificationType.CitizenId,
                Address = "N/S/L woreda 12, H.No 3459",
                IDNumber = "1212",
                TinNumber = "1212",
                Status = UserStatus.Active,
                Occupation = "Head manager",
                StartDate = current,
                IsPermanent = true,
            });
        }
    }
}
