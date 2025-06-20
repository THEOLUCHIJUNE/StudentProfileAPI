using StudentProfileAPI.Application.Interfaces;
using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace StudentProfileAPI.Infrastructure.Persistence
{
    public class DepartmentService : IDepartmentService
    {
        private readonly StudentProfileDbContext _context;

        public DepartmentService(StudentProfileDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.Include(d => d.Faculty).ToListAsync();
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.Include(d => d.Faculty)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);
        }

        public async Task AddDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DepartmentExistsAsync(int id)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentId == id);
        }
    }
}