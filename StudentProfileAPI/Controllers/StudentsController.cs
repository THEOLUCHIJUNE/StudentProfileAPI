using Microsoft.AspNetCore.Mvc;
using StudentProfileAPI.Domain.Models; 
using StudentProfileAPI.Application.Features.Queries; 
using StudentProfileAPI.Application.Features.Commands; 
using System.Collections.Generic; 
using System.Linq; 

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    // Inject all necessary Use Case classes
    private readonly GetStudentsQuery _getStudentsQuery;
    private readonly GetStudentByIdQuery _getStudentByIdQuery;
    private readonly GetStudentByMatricNumberQuery _getStudentByMatricNumberQuery;
    private readonly GetStudentsByStateOfOriginQuery _getStudentsByStateOfOriginQuery;
    private readonly CreateStudentCommand _createStudentCommand;
    private readonly UpdateStudentCommand _updateStudentCommand;
    private readonly DeleteStudentCommand _deleteStudentCommand;

    // Update constructor to inject all use cases
    public StudentsController(GetStudentsQuery getStudentsQuery,
                              GetStudentByIdQuery getStudentByIdQuery,
                              GetStudentByMatricNumberQuery getStudentByMatricNumberQuery,
                              GetStudentsByStateOfOriginQuery getStudentsByStateOfOriginQuery,
                              CreateStudentCommand createStudentCommand,
                              UpdateStudentCommand updateStudentCommand,
                              DeleteStudentCommand deleteStudentCommand)
    {
        _getStudentsQuery = getStudentsQuery;
        _getStudentByIdQuery = getStudentByIdQuery;
        _getStudentByMatricNumberQuery = getStudentByMatricNumberQuery;
        _getStudentsByStateOfOriginQuery = getStudentsByStateOfOriginQuery;
        _createStudentCommand = createStudentCommand;
        _updateStudentCommand = updateStudentCommand;
        _deleteStudentCommand = deleteStudentCommand;
    }

    // GET: api/Students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        // Delegate directly to the GetStudentsQuery use case
        var students = await _getStudentsQuery.ExecuteAsync();
        return Ok(students);
    }

    // GET: api/Students/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        // Delegate to the GetStudentByIdQuery use case
        var student = await _getStudentByIdQuery.ExecuteAsync(id);
        if (student == null)
        {
            return NotFound(); // Return 404 if student not found
        }
        return Ok(student);
    }

    // GET: api/Students/byState?stateOfOrigin=Lagos
    [HttpGet("byState")]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByStateOfOrigin([FromQuery] string stateOfOrigin)
    {
        if (string.IsNullOrWhiteSpace(stateOfOrigin))
        {
            return BadRequest("State of origin cannot be empty.");
        }
        // Delegate to the GetStudentsByStateOfOriginQuery use case
        var students = await _getStudentsByStateOfOriginQuery.ExecuteAsync(stateOfOrigin);
        if (!students.Any())
        {
            return NotFound($"No students found from state: {stateOfOrigin}");
        }
        return Ok(students);
    }

    // GET: api/Students/byMatricNumber/{matricNumber}
    [HttpGet("byMatricNumber/{matricNumber}")]
    public async Task<ActionResult<Student>> GetStudentByMatricNumber(string matricNumber)
    {
        if (string.IsNullOrWhiteSpace(matricNumber))
        {
            return BadRequest("Matric number cannot be empty.");
        }
        // Delegate to the GetStudentByMatricNumberQuery use case
        var student = await _getStudentByMatricNumberQuery.ExecuteAsync(matricNumber);
        if (student == null)
        {
            return NotFound($"Student with matric number {matricNumber} not found.");
        }
        return Ok(student);
    }

    // PUT: api/Students/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, Student student)
    {
        if (id != student.StudentId)
        {
            return BadRequest("Student ID in URL does not match body.");
        }

        // First, check if the student actually exists using a query
        var existingStudent = await _getStudentByIdQuery.ExecuteAsync(id);
        if (existingStudent == null)
        {
            return NotFound($"Student with ID {id} not found.");
        }

        // Update properties of the existing student from the incoming student object
        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.DateOfBirth = student.DateOfBirth;
        existingStudent.StateOfOrigin = student.StateOfOrigin;
        existingStudent.DepartmentId = student.DepartmentId; 

        // Delegate to the UpdateStudentCommand use case
        await _updateStudentCommand.ExecuteAsync(existingStudent); // Pass the updated existing entity
        return NoContent(); // 204 No Content for successful update
    }

    // PUT: api/Students/byMatricNumber/{matricNumber}
    [HttpPut("byMatricNumber/{matricNumber}")]
    public async Task<IActionResult> PutStudentByMatricNumber(string matricNumber, Student updatedStudent)
    {
        if (string.IsNullOrWhiteSpace(matricNumber))
        {
            return BadRequest("Matric number cannot be empty.");
        }

        // Find the existing student by matric number using a query
        var existingStudent = await _getStudentByMatricNumberQuery.ExecuteAsync(matricNumber);
        if (existingStudent == null)
        {
            return NotFound($"Student with matric number {matricNumber} not found.");
        }

        // Update properties of the existing student with the new values
        existingStudent.FirstName = updatedStudent.FirstName;
        existingStudent.LastName = updatedStudent.LastName;
        existingStudent.DateOfBirth = updatedStudent.DateOfBirth;
        existingStudent.StateOfOrigin = updatedStudent.StateOfOrigin;
        existingStudent.DepartmentId = updatedStudent.DepartmentId; // Update department ID if needed

        // Delegate to the UpdateStudentCommand use case
        await _updateStudentCommand.ExecuteAsync(existingStudent);
        return NoContent();
    }

    // POST: api/Students
    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
        // Delegate to the CreateStudentCommand use case
        await _createStudentCommand.ExecuteAsync(student);
        // Assuming StudentId is populated by the database after AddStudentAsync due to EF Core's conventions
        return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
    }

    // DELETE: api/Students/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        // Check if the student exists before attempting to delete using a query
        var studentExists = await _getStudentByIdQuery.ExecuteAsync(id);
        if (studentExists == null)
        {
            return NotFound(); // Return 404 if student not found
        }

        // Delegate to the DeleteStudentCommand use case
        await _deleteStudentCommand.ExecuteAsync(id);
        return NoContent(); // 204 No Content for successful deletion
    }

    
}