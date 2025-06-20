using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Commands
{
    public class UpdateStudentCommand
    {
        private readonly IStudentService _studentService;

        public UpdateStudentCommand(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task ExecuteAsync(Student student)
        {
            await _studentService.UpdateStudentAsync(student);
        }
    }
}