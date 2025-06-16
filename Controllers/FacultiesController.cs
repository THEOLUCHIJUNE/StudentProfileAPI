﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProfileAPI.Data;
using StudentProfileAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class FacultiesController : ControllerBase
{
    private readonly StudentProfileDbContext _context;

    public FacultiesController(StudentProfileDbContext context)
    {
        _context = context;
    }

    // GET: api/Faculties
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculties()
    {
        return await _context.Faculties.ToListAsync();
    }

    // GET: api/Faculties/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Faculty>> GetFaculty(int id)
    {
        var faculty = await _context.Faculties.FindAsync(id);

        if (faculty == null)
        {
            return NotFound();
        }

        return faculty;
    }

    // POST: api/Faculties
    [HttpPost]
    public async Task<ActionResult<Faculty>> PostFaculty(Faculty faculty)
    {
        _context.Faculties.Add(faculty);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFaculty), new { id = faculty.FacultyId }, faculty);
    }

    // PUT: api/Faculties/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFaculty(int id, Faculty faculty)
    {
        if (id != faculty.FacultyId)
        {
            return BadRequest();
        }

        _context.Entry(faculty).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FacultyExists(id))
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

    // DELETE: api/Faculties/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFaculty(int id)
    {
        var faculty = await _context.Faculties.FindAsync(id);
        if (faculty == null)
        {
            return NotFound();
        }

        _context.Faculties.Remove(faculty);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FacultyExists(int id)
    {
        return _context.Faculties.Any(e => e.FacultyId == id);
    }
}