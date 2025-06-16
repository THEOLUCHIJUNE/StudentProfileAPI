using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProfileAPI.Data;
using StudentProfileAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly StudentProfileDbContext _context;

    public StudentsController(StudentProfileDbContext context)
    {
        _context = context;
    }

    // GET: api/Students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _context.Students.Include(s => s.Department).ToListAsync();
    }

    // GET: api/Students
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students.Include(s => s.Department).FirstOrDefaultAsync(s => s.StudentId == id);

        if (student == null)
        {
            return NotFound();
        }

        return student;
    }

    // POST: api/Students
    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
    }

    // PUT: api/Students
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, Student student)
    {
        if (id != student.StudentId)
        {
            return BadRequest();
        }

        _context.Entry(student).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Students/
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/Students/byState?stateOfOrigin=Lagos
    [HttpGet("byState")]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByStateOfOrigin([FromQuery] string stateOfOrigin)
    {
        if (string.IsNullOrWhiteSpace(stateOfOrigin))
        {
            return BadRequest("State of Origin cannot be empty.");
        }

        // Case-insensitive comparison for state of origin
        var students = await _context.Students
                                     .Include(s => s.Department)
                                     .Where(s => s.StateOfOrigin.ToLower() == stateOfOrigin.ToLower())
                                     .ToListAsync();

        if (!students.Any())
        {
            return NotFound($"No students found for State of Origin: {stateOfOrigin}");
        }

        return students;
    }

    // GET: api/Students/byMatricNumber
    [HttpGet("byMatricNumber/{matricNumber}")]
    public async Task<ActionResult<Student>> GetStudentByMatricNumber(string matricNumber)
    {
        if (string.IsNullOrWhiteSpace(matricNumber))
        {
            return BadRequest("Matric Number cannot be empty.");
        }

        var student = await _context.Students
                                     .Include(s => s.Department)
                                     .FirstOrDefaultAsync(s => s.MatricNumber.ToLower() == matricNumber.ToLower());

        if (student == null)
        {
            return NotFound($"Student with Matric Number '{matricNumber}' not found.");
        }

        return student;
    }

    // PUT: api/Students/byMatricNumber/
    [HttpPut("byMatricNumber/{matricNumber}")]
    public async Task<IActionResult> PutStudentByMatricNumber(string matricNumber, Student updatedStudent)
    {
        if (string.IsNullOrWhiteSpace(matricNumber))
        {
            return BadRequest("Matric Number in URL cannot be empty.");
        }

        // Find the existing student by matric number
        var existingStudent = await _context.Students
                                            .FirstOrDefaultAsync(s => s.MatricNumber.ToLower() == matricNumber.ToLower());

        if (existingStudent == null)
        {
            return NotFound($"Student with Matric Number '{matricNumber}' not found.");
        }

        // Ensure the ID from the URL matches the ID of the found student,
        // or handle cases where the ID in the body might be different.
        // For simplicity, we'll ensure the ID in the body matches the found student's ID.
        if (updatedStudent.StudentId != 0 && updatedStudent.StudentId != existingStudent.StudentId)
        {
            return BadRequest("Student ID in body does not match existing student's ID for this matric number.");
        }

        // Update the properties of the existing student from the updatedStudent object
        existingStudent.FirstName = updatedStudent.FirstName;
        existingStudent.LastName = updatedStudent.LastName;
        existingStudent.StateOfOrigin = updatedStudent.StateOfOrigin;
        existingStudent.DateOfBirth = updatedStudent.DateOfBirth;
        existingStudent.DepartmentId = updatedStudent.DepartmentId;
        // Do NOT update MatricNumber itself via this endpoint if it's considered immutable after creation
        // If MatricNumber can change, add: existingStudent.MatricNumber = updatedStudent.MatricNumber;

        _context.Entry(existingStudent).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // This catch block is more relevant if multiple users are trying to update the same record
            // simultaneously. For a single-user scenario, it might not be hit often.
            if (!StudentExists(existingStudent.StudentId)) // Use the original student's ID to check existence
            {
                return NotFound();
            }
            else
            {
                throw; // Re-throw if it's a genuine concurrency issue
            }
        }

        return NoContent(); // 204 No Content, indicating success with no response body
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.StudentId == id);
    }
}