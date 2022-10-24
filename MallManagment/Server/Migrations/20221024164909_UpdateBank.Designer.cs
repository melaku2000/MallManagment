﻿// <auto-generated />
using System;
using MallManagment.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MallManagment.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221024164909_UpdateBank")]
    partial class UpdateBank
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence<int>("MySequence", "shared")
                .StartsAt(1000L);

            modelBuilder.Entity("MallManagment.Shared.Models.Adminstrator", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastLoginTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("LockCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("PhoneConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.ToTable("Adminstrators");

                    b.HasData(
                        new
                        {
                            EmployeeId = "melaku1234",
                            CreatedDate = new DateTime(2022, 10, 24, 16, 49, 8, 992, DateTimeKind.Utc).AddTicks(7422),
                            Email = "melakumen@gmail.com",
                            EmailConfirmed = true,
                            LastLoginTime = new DateTime(2022, 10, 24, 16, 49, 8, 992, DateTimeKind.Utc).AddTicks(7422),
                            LockCount = 0,
                            ModifyDate = new DateTime(2022, 10, 24, 16, 49, 8, 992, DateTimeKind.Utc).AddTicks(7422),
                            PasswordHash = new byte[] { 249, 106, 8, 203, 194, 162, 77, 142, 142, 15, 176, 57, 83, 218, 220, 176, 139, 118, 253, 231, 200, 67, 129, 101 },
                            PasswordSalt = new byte[] { 188, 182, 228, 78, 214, 30, 42, 234, 181, 137, 177, 151, 245, 71, 96, 191, 207, 126, 45, 10, 167, 196, 139, 105 },
                            PhoneConfirmed = false,
                            Role = 101,
                            Status = 111
                        });
                });

            modelBuilder.Entity("MallManagment.Shared.Models.Bank", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContactMobilePerson")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OfficeAddress")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("OfficePhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("MallManagment.Shared.Models.BankReading", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BankId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CurrentReading")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("BankReadings");
                });

            modelBuilder.Entity("MallManagment.Shared.Models.BusinessCatagory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CatagoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BusinessCatagories");
                });

            modelBuilder.Entity("MallManagment.Shared.Models.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CatagoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CompanyTin")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("CompanyUrl")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("IDNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("IdentificationType")
                        .HasColumnType("int");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OfficePhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TinNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CatagoryId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MallManagment.Shared.Models.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR shared.MySequence");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("IDNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("IdentificationType")
                        .HasColumnType("int");

                    b.Property<bool>("IsPermanent")
                        .HasColumnType("bit");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TinNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = "melaku1234",
                            Address = "N/S/L woreda 12, H.No 3459",
                            CreatedDate = new DateTime(2022, 10, 24, 16, 49, 8, 992, DateTimeKind.Utc).AddTicks(7139),
                            EmployeeNumber = 0,
                            FullName = "Melaku Michael",
                            IDNumber = "1212",
                            IdentificationType = 121,
                            IsPermanent = true,
                            MobilePhone = "0911641927",
                            ModifyDate = new DateTime(2022, 10, 24, 16, 49, 8, 992, DateTimeKind.Utc).AddTicks(7139),
                            Occupation = "Head manager",
                            Salary = 0m,
                            StartDate = new DateTime(2022, 10, 24, 16, 49, 8, 992, DateTimeKind.Utc).AddTicks(7139),
                            Status = 111,
                            TinNumber = "1212"
                        });
                });

            modelBuilder.Entity("MallManagment.Shared.Models.Adminstrator", b =>
                {
                    b.HasOne("MallManagment.Shared.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("MallManagment.Shared.Models.BankReading", b =>
                {
                    b.HasOne("MallManagment.Shared.Models.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MallManagment.Shared.Models.Employee", "Employee")
                        .WithMany("BankReadings")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("MallManagment.Shared.Models.Customer", b =>
                {
                    b.HasOne("MallManagment.Shared.Models.BusinessCatagory", "Catagory")
                        .WithMany("Clients")
                        .HasForeignKey("CatagoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Catagory");
                });

            modelBuilder.Entity("MallManagment.Shared.Models.BusinessCatagory", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("MallManagment.Shared.Models.Employee", b =>
                {
                    b.Navigation("BankReadings");
                });
#pragma warning restore 612, 618
        }
    }
}