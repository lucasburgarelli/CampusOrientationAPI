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

        return Ok(courses);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetCourseByRaAsync([FromQuery] String id)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid entry");

        var courses = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        return Ok(courses);
    }

    [HttpPut("ra")]
    public async Task<IActionResult> PutCourseAsync([FromQuery] String ra)
    {
        if (!ModelState.IsValid) return BadRequest();

        var student = await _context.People.FirstOrDefaultAsync(p => p.Ra == ra);
        if (student == null) return BadRequest("Student not found");

        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var studies = await _context.Studies.AsNoTracking().Where(s => s.IdStudent == student.Id).ToListAsync();

            if (studies == null) Ok("Student without courses");

            var count = studies!.Count;
            _context.Studies.RemoveRange(studies);

            var courses = await _context.Courses.AsNoTracking().ToListAsync();
            var randomized = new List<Course>();
            randomized.AddRange(courses.OrderBy(i => new Random().Next()).Take(count));

            randomized.ForEach(async c => await _context.Studies.AddAsync(new Study
            {
                IdCourse = c.Id,
                IdStudent = student.Id
            }));

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return Ok("Course update suceded");
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return BadRequest(e.Message);
        }
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
