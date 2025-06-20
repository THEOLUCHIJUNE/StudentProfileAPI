using StudentProfileAPI.Application.Interfaces;           
using StudentProfileAPI.Domain.Models;                   
using StudentProfileAPI.Infrastructure.Data;            
using Microsoft.EntityFrameworkCore;                   

namespace StudentProfileAPI.Infrastructure.Persistence
{
    public class StudentService : IStudentService
    {
        private readonly StudentProfileDbContext _context;

        public StudentService(StudentProfileDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.Include(s => s.Department).ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.StudentId == id);
        }

        public async Task<Student?> GetStudentByMatricNumberAsync(string matricNumber)
        {
            return await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.MatricNumber.ToLower() == matricNumber.ToLower());
        }

        public async Task AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> StudentExistsAsync(int id)
        {
            return await _context.Students.AnyAsync(e => e.StudentId == id);
        }

        public async Task<IEnumerable<Student>> GetStudentsByStateOfOriginAsync(string stateOfOrigin)
        {
            return await _context.Students
                .Include(s => s.Department)
                .Where(s => s.StateOfOrigin.ToLower() == stateOfOrigin.ToLower())
                .ToListAsync();
        }
    }
}