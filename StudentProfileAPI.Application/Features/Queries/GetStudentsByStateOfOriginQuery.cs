using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Queries
{
    public class GetStudentsByStateOfOriginQuery
    {
        private readonly IStudentService _studentService;

        public GetStudentsByStateOfOriginQuery(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IEnumerable<Student>> ExecuteAsync(string stateOfOrigin) // Takes the state to filter by
        {
            return await _studentService.GetStudentsByStateOfOriginAsync(stateOfOrigin);
        }
    }
}