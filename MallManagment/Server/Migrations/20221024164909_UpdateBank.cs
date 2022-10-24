using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MallManagment.Server.Migrations
{
    public partial class UpdateBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Adminstrators",
                keyColumn: "EmployeeId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "LastLoginTime", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 10, 24, 16, 49, 8, 992, DateTimeKind.Utc).AddTicks(7422), new DateTime(2022, 10, 24, 16, 49, 8, 992, DateTimeKind.Utc).AddTicks(7422), new DateTime(2022, 10, 24, 16, 49, 8, 992, DateTimeKind.Utc).AddTicks(7422), new byte[] { 249, 106, 8, 203, 194, 162, 77, 142, 142, 15, 176, 57, 83, 218, 220, 176, 139, 118, 253, 231, 200, 67, 129, 101 }, new byte[] { 188, 182, 228, 78, 214, 30, 42, 234, 181, 137, 177, 151, 245, 71, 96, 191, 207, 126, 45, 10, 167, 196, 139, 105 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "StartDate" },
                values: new object[] { new DateTime(2022, 10, 24, 16, 49, 8, 992, DateTimeKind.Utc).AddTicks(7139), new DateTime(2022, 10, 24, 16, 49, 8, 992, DateTimeKind.Utc).AddTicks(7139), new DateTime(2022, 10, 24, 16, 49, 8, 992, DateTimeKind.Utc).AddTicks(7139) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Adminstrators",
                keyColumn: "EmployeeId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "LastLoginTime", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 10, 23, 20, 21, 45, 758, DateTimeKind.Utc).AddTicks(8139), new DateTime(2022, 10, 23, 20, 21, 45, 758, DateTimeKind.Utc).AddTicks(8139), new DateTime(2022, 10, 23, 20, 21, 45, 758, DateTimeKind.Utc).AddTicks(8139), new byte[] { 227, 243, 43, 143, 158, 69, 241, 58, 136, 82, 8, 97, 140, 150, 136, 224, 193, 214, 55, 212, 65, 67, 224, 190 }, new byte[] { 62, 28, 29, 209, 17, 211, 113, 100, 75, 132, 159, 0, 29, 5, 181, 162, 49, 221, 58, 16, 100, 222, 151, 111 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "StartDate" },
                values: new object[] { new DateTime(2022, 10, 23, 20, 21, 45, 758, DateTimeKind.Utc).AddTicks(7677), new DateTime(2022, 10, 23, 20, 21, 45, 758, DateTimeKind.Utc).AddTicks(7677), new DateTime(2022, 10, 23, 20, 21, 45, 758, DateTimeKind.Utc).AddTicks(7677) });
        }
    }
}
