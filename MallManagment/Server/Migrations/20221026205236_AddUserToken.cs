using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MallManagment.Server.Migrations
{
    public partial class AddUserToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenType = table.Column<int>(type: "int", nullable: false),
                    TokenExpireTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Adminstrators",
                keyColumn: "EmployeeId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "LastLoginTime", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 10, 26, 20, 52, 35, 502, DateTimeKind.Utc).AddTicks(2380), new DateTime(2022, 10, 26, 20, 52, 35, 502, DateTimeKind.Utc).AddTicks(2380), new DateTime(2022, 10, 26, 20, 52, 35, 502, DateTimeKind.Utc).AddTicks(2380), new byte[] { 152, 94, 110, 76, 237, 54, 22, 69, 51, 205, 6, 75, 214, 115, 231, 68, 76, 225, 39, 158, 83, 159, 76, 43 }, new byte[] { 115, 182, 69, 154, 164, 230, 63, 78, 234, 128, 16, 225, 154, 1, 144, 109, 64, 123, 147, 147, 173, 98, 40, 171 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "StartDate" },
                values: new object[] { new DateTime(2022, 10, 26, 20, 52, 35, 502, DateTimeKind.Utc).AddTicks(2092), new DateTime(2022, 10, 26, 20, 52, 35, 502, DateTimeKind.Utc).AddTicks(2092), new DateTime(2022, 10, 26, 20, 52, 35, 502, DateTimeKind.Utc).AddTicks(2092) });

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_EmployeeId",
                table: "UserTokens",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTokens");

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
    }
}
