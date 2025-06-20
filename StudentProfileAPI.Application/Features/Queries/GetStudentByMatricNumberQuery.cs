using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Queries
{
    public class GetStudentByMatricNumberQuery
    {
        private readonly IStudentService _studentService;

        public GetStudentByMatricNumberQuery(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<Student?> ExecuteAsync(string matricNumber) // Takes the matric number to search for
        {
            return await _studentService.GetStudentByMatricNumberAsync(matricNumber);
        }
    }
}