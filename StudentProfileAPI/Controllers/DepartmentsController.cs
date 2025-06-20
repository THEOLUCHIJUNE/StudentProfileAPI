using Microsoft.AspNetCore.Mvc;
using StudentProfileAPI.Domain.Models; 
using StudentProfileAPI.Application.Features.Queries; 
using StudentProfileAPI.Application.Features.Commands; 
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    // Inject all necessary Use Case classes for Departments
    private readonly GetDepartmentsQuery _getDepartmentsQuery;
    private readonly GetDepartmentByIdQuery _getDepartmentByIdQuery;
    private readonly CreateDepartmentCommand _createDepartmentCommand;
    private readonly UpdateDepartmentCommand _updateDepartmentCommand;
    private readonly DeleteDepartmentCommand _deleteDepartmentCommand;

    // Constructor to inject all department-related use cases
    public DepartmentsController(GetDepartmentsQuery getDepartmentsQuery,
                                 GetDepartmentByIdQuery getDepartmentByIdQuery,
                                 CreateDepartmentCommand createDepartmentCommand,
                                 UpdateDepartmentCommand updateDepartmentCommand,
                                 DeleteDepartmentCommand deleteDepartmentCommand)
    {
        _getDepartmentsQuery = getDepartmentsQuery;
        _getDepartmentByIdQuery = getDepartmentByIdQuery;
        _createDepartmentCommand = createDepartmentCommand;
        _updateDepartmentCommand = updateDepartmentCommand;
        _deleteDepartmentCommand = deleteDepartmentCommand;
    }

    // GET: api/Departments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        var departments = await _getDepartmentsQuery.ExecuteAsync();
        return Ok(departments);
    }

    // GET: api/Departments/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartment(int id)
    {
        var department = await _getDepartmentByIdQuery.ExecuteAsync(id);
        if (department == null)
        {
            return NotFound();
        }
        return Ok(department);
    }

    // PUT: api/Departments/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(int id, Department department)
    {
        if (id != department.DepartmentId)
        {
            return BadRequest("Department ID in URL does not match body.");
        }

        var existingDepartment = await _getDepartmentByIdQuery.ExecuteAsync(id);
        if (existingDepartment == null)
        {
            return NotFound($"Department with ID {id} not found.");
        }

        // Update properties of the existing department
        existingDepartment.DepartmentName = department.DepartmentName;
        existingDepartment.FacultyId = department.FacultyId; 

        await _updateDepartmentCommand.ExecuteAsync(existingDepartment);
        return NoContent();
    }

    // POST: api/Departments
    [HttpPost]
    public async Task<ActionResult<Department>> PostDepartment(Department department)
    {
        await _createDepartmentCommand.ExecuteAsync(department);
        return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, department);
    }

    // DELETE: api/Departments/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var departmentExists = await _getDepartmentByIdQuery.ExecuteAsync(id);
        if (departmentExists == null)
        {
            return NotFound();
        }

        await _deleteDepartmentCommand.ExecuteAsync(id);
        return NoContent();
    }
}