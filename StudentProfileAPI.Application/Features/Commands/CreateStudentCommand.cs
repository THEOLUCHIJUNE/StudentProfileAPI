using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

public class CreateStudentCommand
{
    private readonly IStudentService _studentService; // Injects the service

    public CreateStudentCommand(IStudentService studentService) // Constructor updated
    {
        _studentService = studentService;
    }

    public async Task ExecuteAsync(Student student)
    {
        await _studentService.AddStudentAsync(student); // Uses the service
    }
}