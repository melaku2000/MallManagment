using MallManagment.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallManagment.Server.Handlers;

namespace MallManagment.Server.Data
{
    public class AdminConfiguration : IEntityTypeConfiguration<Adminstrator>
    {
        public void Configure(EntityTypeBuilder<Adminstrator> builder)
        {
            var current = DateTime.UtcNow;
            byte[] hash, salt;

            PasswordHasher.GeneratePasswordHasing("Melaku@12", out salt, out hash);
           
            builder.HasData(new Adminstrator
            {
                EmployeeId = "melaku1234",
                Email = "melakumen@gmail.com",
                CreatedDate = current,
                ModifyDate = current,
                EmailConfirmed = true,
                Role = Shared.Enums.RoleType.SuperAdmin,
                PasswordHash = hash,
                PasswordSalt = salt, LastLoginTime=current, Status=Shared.Enums.UserStatus.Active
            });
        }
    }
}
