using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Queries
{
    public class GetStudentByIdQuery
    {
        private readonly IStudentService _studentService;

        public GetStudentByIdQuery(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<Student?> ExecuteAsync(int studentId) // Takes the ID to search for
        {
            return await _studentService.GetStudentByIdAsync(studentId);
        }
    }
}