using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

public class GetStudentsQuery
{
    private readonly IStudentService _studentService; // Injects the service

    public GetStudentsQuery(IStudentService studentService) // Updated the constructor 
    {
        _studentService = studentService;
    }

    public async Task<IEnumerable<Student>> ExecuteAsync()
    {
        return await _studentService.GetAllStudentsAsync(); // Uses the service
    }
}