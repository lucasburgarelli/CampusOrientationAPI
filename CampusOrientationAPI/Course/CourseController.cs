using CampusOrientationAPI.Data;
using CampusOrientationAPI.Models;
using CampusOrientationAPI.People;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusOrientationAPI.Courses;

[ApiController]
[Route("api/course")]
public sealed class CourseController : Controller
{
    private readonly CampusOrientationDBContext _context;

    public CourseController(CampusOrientationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var courses = await _context.Courses.AsNoTracking().ToListAsync();

        return courses == null ? NotFound() : Ok(courses);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetCourseByRaAsync([FromBody] String id)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid entry");

        var courses = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        return courses == null ? NotFound() : Ok(courses);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CourseViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        try
        {
            await _context.Courses.AddAsync(new Course
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Description = model.Description,
                Idteacher = model.Idteacher
            });
            await _context.SaveChangesAsync();
            return Ok("Course add suceded");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
