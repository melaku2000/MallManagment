using MallManagment.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MallManagment.Server.Data
{
    public class AppDbContext : DbContext
    {
        public IConfiguration config { get; }
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options)
        {
            this.config = config;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());

            modelBuilder.HasSequence<int>("MySequence", schema: "shared").StartsAt(1000).IncrementsBy(1);
            modelBuilder.Entity<Employee>()
           .Property(o => o.EmployeeNumber)
           .HasDefaultValueSql("NEXT VALUE FOR shared.MySequence");

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList().ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(config.GetConnectionString("FanaDbConnection"),
                b => b.MigrationsAssembly("MallManagment.Server"));//services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString));
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Adminstrator> Adminstrators { get; set; } = null!;
        public DbSet<BusinessCatagory> BusinessCatagories { get; set; } = null!;
        public DbSet<Bank> Banks { get; set; } = null!;
        public DbSet<BankReading> BankReadings { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
       
        //public DbSet<Approval> Approvals { get; set; } = null!;
        //public DbSet<Message> Messages { get; set; } = null!;
        //public DbSet<RentalMessage> RentalMessages { get; set; } = null!;

    }
}
