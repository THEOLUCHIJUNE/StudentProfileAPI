using StudentProfileAPI.Application.Interfaces;
using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace StudentProfileAPI.Infrastructure.Persistence
{
    public class FacultyService : IFacultyService
    {
        private readonly StudentProfileDbContext _context;

        public FacultyService(StudentProfileDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Faculty>> GetAllFacultiesAsync()
        {
            return await _context.Faculties.ToListAsync();
        }

        public async Task<Faculty?> GetFacultyByIdAsync(int id)
        {
            return await _context.Faculties.FirstOrDefaultAsync(f => f.FacultyId == id);
        }

        public async Task AddFacultyAsync(Faculty faculty)
        {
            _context.Faculties.Add(faculty);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFacultyAsync(Faculty faculty)
        {
            _context.Entry(faculty).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFacultyAsync(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> FacultyExistsAsync(int id)
        {
            return await _context.Faculties.AnyAsync(f => f.FacultyId == id);
        }
    }
}
