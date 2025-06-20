using StudentProfileAPI.Domain.Models;

namespace StudentProfileAPI.Application.Interfaces
{
    public interface IFacultyService
    {
        Task<IEnumerable<Faculty>> GetAllFacultiesAsync();
        Task<Faculty?> GetFacultyByIdAsync(int id);
        Task AddFacultyAsync(Faculty faculty);
        Task UpdateFacultyAsync(Faculty faculty);
        Task DeleteFacultyAsync(int id);
        Task<bool> FacultyExistsAsync(int id);
    }
}

