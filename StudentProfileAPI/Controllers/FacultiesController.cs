using Microsoft.AspNetCore.Mvc;
using StudentProfileAPI.Domain.Models; // Your models are now in Domain
using StudentProfileAPI.Application.Features.Queries; // All your Faculty queries
using StudentProfileAPI.Application.Features.Commands; // All your Faculty commands
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class FacultiesController : ControllerBase
{
    // Inject all necessary Use Case classes for Faculties
    private readonly GetFacultiesQuery _getFacultiesQuery;
    private readonly GetFacultyByIdQuery _getFacultyByIdQuery;
    private readonly CreateFacultyCommand _createFacultyCommand;
    private readonly UpdateFacultyCommand _updateFacultyCommand;
    private readonly DeleteFacultyCommand _deleteFacultyCommand;

    // Constructor to inject all faculty-related use cases
    public FacultiesController(GetFacultiesQuery getFacultiesQuery,
                               GetFacultyByIdQuery getFacultyByIdQuery,
                               CreateFacultyCommand createFacultyCommand,
                               UpdateFacultyCommand updateFacultyCommand,
                               DeleteFacultyCommand deleteFacultyCommand)
    {
        _getFacultiesQuery = getFacultiesQuery;
        _getFacultyByIdQuery = getFacultyByIdQuery;
        _createFacultyCommand = createFacultyCommand;
        _updateFacultyCommand = updateFacultyCommand;
        _deleteFacultyCommand = deleteFacultyCommand;
    }

    // GET: api/Faculties
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculties()
    {
        var faculties = await _getFacultiesQuery.ExecuteAsync();
        return Ok(faculties);
    }

    // GET: api/Faculties/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Faculty>> GetFaculty(int id)
    {
        var faculty = await _getFacultyByIdQuery.ExecuteAsync(id);
        if (faculty == null)
        {
            return NotFound();
        }
        return Ok(faculty);
    }

    // PUT: api/Faculties/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFaculty(int id, Faculty faculty)
    {
        if (id != faculty.FacultyId)
        {
            return BadRequest("Faculty ID in URL does not match body.");
        }

        var existingFaculty = await _getFacultyByIdQuery.ExecuteAsync(id);
        if (existingFaculty == null)
        {
            return NotFound($"Faculty with ID {id} not found.");
        }

        // Update properties of the existing faculty
        existingFaculty.FacultyName = faculty.FacultyName;

        await _updateFacultyCommand.ExecuteAsync(existingFaculty);
        return NoContent();
    }

    // POST: api/Faculties
    [HttpPost]
    public async Task<ActionResult<Faculty>> PostFaculty(Faculty faculty)
    {
        await _createFacultyCommand.ExecuteAsync(faculty);
        return CreatedAtAction(nameof(GetFaculty), new { id = faculty.FacultyId }, faculty);
    }

    // DELETE: api/Faculties/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFaculty(int id)
    {
        var facultyExists = await _getFacultyByIdQuery.ExecuteAsync(id);
        if (facultyExists == null)
        {
            return NotFound();
        }

        await _deleteFacultyCommand.ExecuteAsync(id);
        return NoContent();
    }
}