using StudentProfileAPI.Domain.Models;

namespace StudentProfileAPI.Application.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student?> GetStudentByMatricNumberAsync(string matricNumber);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task<bool> StudentExistsAsync(int id); // Changed to async
        Task<IEnumerable<Student>> GetStudentsByStateOfOriginAsync(string stateOfOrigin);
    }
}