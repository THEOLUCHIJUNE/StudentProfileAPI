using Microsoft.EntityFrameworkCore;
using StudentProfileAPI.Domain.Models; 

namespace StudentProfileAPI.Infrastructure.Data 
{
    public class StudentProfileDbContext : DbContext
    {
        public StudentProfileDbContext(DbContextOptions<StudentProfileDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentName).HasColumnName("Name"); 
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.Property(e => e.FacultyName).HasColumnName("Name"); 
            });
        }
    }
}