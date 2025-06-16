using Microsoft.EntityFrameworkCore;
using StudentProfileAPI.Models;

namespace StudentProfileAPI.Data
{
    public class StudentProfileDbContext : DbContext
    {
        public StudentProfileDbContext(DbContextOptions<StudentProfileDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //this defines relationships between tables and sets custom rules for how they react
        {
            // Configure relationships (though EF Core often infers these)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Faculty)
                .WithMany(f => f.Departments)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.Cascade); // Optional

            
        }
    }
}