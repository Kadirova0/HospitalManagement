using HospitalManagement.Contracts;
using HospitalManagement.Database.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Drawing;

namespace HospitalManagement.Database
{
    public class HospitalDbContext : DbContext
    {

        public HospitalDbContext(DbContextOptions dbContextOptions) 
            : base(dbContextOptions) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder
                .Entity<User>()
                .HasData(
                 new User
                 {
                    Id= 1,
                    Name="Admin",
                    Surname="Admin",
                    Email = "admin@gmail.com",
                    Password = "admin"
                 });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
