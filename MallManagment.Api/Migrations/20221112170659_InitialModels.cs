﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MallManagment.Api.Migrations
{
    public partial class InitialModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.CreateSequence<int>(
                name: "MySequence",
                schema: "shared",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactMobilePerson = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OfficePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OfficeAddress = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCatagories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CatagoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCatagories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeNumber = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR shared.MySequence"),
                    Occupation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsPermanent = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentificationType = table.Column<int>(type: "int", nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MobilePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TinNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CatagoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyTin = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    OfficePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyUrl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentificationType = table.Column<int>(type: "int", nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MobilePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TinNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_BusinessCatagories_CatagoryId",
                        column: x => x.CatagoryId,
                        principalTable: "BusinessCatagories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Adminstrators",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PhoneConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockCount = table.Column<int>(type: "int", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LastLoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminstrators", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Adminstrators_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankReadings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentReading = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankReadings_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankReadings_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "CreatedDate", "EndDate", "FullName", "IDNumber", "IdentificationType", "IsPermanent", "MobilePhone", "ModifyDate", "Occupation", "Salary", "StartDate", "Status", "TinNumber" },
                values: new object[] { "melaku1234", "N/S/L woreda 12, H.No 3459", new DateTime(2022, 11, 12, 17, 6, 59, 272, DateTimeKind.Utc).AddTicks(4332), null, "Melaku Michael", "1212", 121, true, "0911641927", new DateTime(2022, 11, 12, 17, 6, 59, 272, DateTimeKind.Utc).AddTicks(4332), "Head manager", 0m, new DateTime(2022, 11, 12, 17, 6, 59, 272, DateTimeKind.Utc).AddTicks(4332), 111, "1212" });

            migrationBuilder.InsertData(
                table: "Adminstrators",
                columns: new[] { "EmployeeId", "CreatedDate", "Email", "EmailConfirmed", "LastLoginTime", "LockCount", "ModifyDate", "PasswordHash", "PasswordSalt", "PhoneConfirmed", "Role", "Status" },
                values: new object[] { "melaku1234", new DateTime(2022, 11, 12, 17, 6, 59, 272, DateTimeKind.Utc).AddTicks(4539), "melakumen@gmail.com", true, new DateTime(2022, 11, 12, 17, 6, 59, 272, DateTimeKind.Utc).AddTicks(4539), 0, new DateTime(2022, 11, 12, 17, 6, 59, 272, DateTimeKind.Utc).AddTicks(4539), new byte[] { 30, 141, 227, 230, 147, 208, 192, 209, 196, 65, 81, 39, 172, 115, 3, 170, 208, 1, 191, 74, 236, 89, 141, 229 }, new byte[] { 11, 66, 185, 109, 77, 120, 211, 139, 74, 137, 212, 2, 123, 235, 65, 227, 12, 133, 103, 92, 18, 27, 148, 183 }, false, 101, 111 });

            migrationBuilder.CreateIndex(
                name: "IX_BankReadings_BankId",
                table: "BankReadings",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankReadings_EmployeeId",
                table: "BankReadings",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CatagoryId",
                table: "Customers",
                column: "CatagoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_EmployeeId",
                table: "UserTokens",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adminstrators");

            migrationBuilder.DropTable(
                name: "BankReadings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "BusinessCatagories");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropSequence(
                name: "MySequence",
                schema: "shared");
        }
    }
}