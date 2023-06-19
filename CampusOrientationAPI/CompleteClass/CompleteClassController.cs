using CampusOrientationAPI.CompleteClasses;
using CampusOrientationAPI.Data;
using CampusOrientationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging;
using System;
using System.Linq;

namespace CampusOrientationAPI.CompleteClass;

[ApiController]
[Route("api/completeclass")]
public sealed class CompleteClassController : ControllerBase
{
    private readonly CampusOrientationDBContext _context;

    public CompleteClassController(CampusOrientationDBContext context)
    {
        _context = context;
    }

    [HttpGet("ra")]
    public async Task<IActionResult> GetClassesTodayByUserAsync([FromQuery] CompleteClassRaViewModel model)
    {
        var person = await _context.People.FirstOrDefaultAsync(p => p.Ra == model.Ra);
        if (person == null) return BadRequest("Student does not exists");

        var studies = await _context.Studies.AsNoTracking().Where(c => c.IdStudent == person.Id).ToListAsync();

        var coursesIds = studies.Select(c => c.IdCourse).ToList();

        var classes = await _context.Classes
            .Include(c => c.Course).ThenInclude(c => c.Teacher).AsNoTracking()
            .Where(c => coursesIds.Contains(c.Idcourse)).ToListAsync();

        var response = classes.Select(cl => 
            new { cl.Classroom, cl.Datetimestart, cl.Datetimeend, cl.Description, CourseName = cl.Course.Name, Teacher = cl.Course.Teacher!.Name }).ToList();

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClassesAsync()
    {
        var classes = await _context.Completeclasses.AsNoTracking().ToListAsync();

        return classes == null ? NotFound() : Ok(classes);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] Completeclass model)
    {
        if (!ModelState.IsValid) return BadRequest();

        try
        {
            var teacherGuid = Guid.NewGuid().ToString();
            var courseGuid = Guid.NewGuid().ToString();

            var sameNamePerson = await _context.People.AsNoTracking().FirstOrDefaultAsync(p => p.Name == model.Nameteacher);
            if (sameNamePerson is not null)
                teacherGuid = sameNamePerson.Id;
            else
                await _context.People.AddAsync(new Person
                {
                    Id = teacherGuid,
                    Name = model.Nameteacher!
                });

            var sameNameCourse = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Name == model.Coursename);
            if(sameNameCourse is not null)
                courseGuid = sameNameCourse.Id;
            else
                await _context.Courses.AddAsync(new Course
                {
                    Id = courseGuid,
                    Name = model.Coursename!,
                    Idteacher = teacherGuid
                });

            await _context.Classes.AddAsync(new Class
            {
                Idcourse = courseGuid,
                Datetimestart = (DateTime)(model.Datetimestart is null ? DateTime.Now : model.Datetimestart),
                Datetimeend = (DateTime)(model.Datetimeend is null ? DateTime.Now : model.Datetimeend),
                Classroom = model.Classroom,
                Description = model.Description
            });

            await _context.SaveChangesAsync();
            return Ok("Completeclass add suceded");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
