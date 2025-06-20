using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Commands
{
    public class DeleteStudentCommand
    {
        private readonly IStudentService _studentService;

        public DeleteStudentCommand(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task ExecuteAsync(int studentId) // Takes the ID of the student to delete
        {
            await _studentService.DeleteStudentAsync(studentId);
        }
    }
}