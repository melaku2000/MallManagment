using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MallManagment.Api.Migrations
{
    public partial class UpdateAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Adminstrators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Adminstrators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpireTime",
                table: "Adminstrators",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Adminstrators",
                keyColumn: "EmployeeId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "LastLoginTime", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 11, 12, 20, 3, 28, 516, DateTimeKind.Utc).AddTicks(809), new DateTime(2022, 11, 12, 20, 3, 28, 516, DateTimeKind.Utc).AddTicks(809), new DateTime(2022, 11, 12, 20, 3, 28, 516, DateTimeKind.Utc).AddTicks(809), new byte[] { 23, 181, 204, 253, 193, 169, 83, 237, 2, 37, 62, 34, 200, 243, 170, 125, 107, 35, 209, 107, 147, 213, 214, 1 }, new byte[] { 123, 33, 157, 153, 251, 5, 87, 150, 32, 187, 11, 218, 167, 142, 99, 232, 137, 15, 88, 92, 210, 115, 138, 65 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "StartDate" },
                values: new object[] { new DateTime(2022, 11, 12, 20, 3, 28, 516, DateTimeKind.Utc).AddTicks(321), new DateTime(2022, 11, 12, 20, 3, 28, 516, DateTimeKind.Utc).AddTicks(321), new DateTime(2022, 11, 12, 20, 3, 28, 516, DateTimeKind.Utc).AddTicks(321) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Adminstrators");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Adminstrators");

            migrationBuilder.DropColumn(
                name: "TokenExpireTime",
                table: "Adminstrators");

            migrationBuilder.UpdateData(
                table: "Adminstrators",
                keyColumn: "EmployeeId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "LastLoginTime", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 11, 12, 17, 6, 59, 272, DateTimeKind.Utc).AddTicks(4539), new DateTime(2022, 11, 12, 17, 6, 59, 272, DateTimeKind.Utc).AddTicks(4539), new DateTime(2022, 11, 12, 17, 6, 59, 272, DateTimeKind.Utc).AddTicks(4539), new byte[] { 30, 141, 227, 230, 147, 208, 192, 209, 196, 65, 81, 39, 172, 115, 3, 170, 208, 1, 191, 74, 236, 89, 141, 229 }, new byte[] { 11, 66, 185, 109, 77, 120, 211, 139, 74, 137, 212, 2, 123, 235, 65, 227, 12, 133, 103, 92, 18, 27, 148, 183 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "StartDate" },
                values: new object[] { new DateTime(2022, 11, 12, 17, 6, 59, 272, DateTimeKind.Utc).AddTicks(4332), new DateTime(2022, 11, 12, 17, 6, 59, 272, DateTimeKind.Utc).AddTicks(4332), new DateTime(2022, 11, 12, 17, 6, 59, 272, DateTimeKind.Utc).AddTicks(4332) });
        }
    }
}
