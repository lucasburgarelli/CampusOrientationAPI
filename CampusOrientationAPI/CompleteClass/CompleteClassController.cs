using CampusOrientationAPI.Data;
using CampusOrientationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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

            await _context.People.AddAsync(new Person
            {
                Ra = teacherGuid,
                Name = model.Nameteacher!
            });
            await _context.Courses.AddAsync(new Course
            {
                Id = courseGuid,
                Name = model.Coursename!,
                Rateacher = teacherGuid
            });
            await _context.Classes.AddAsync(new Class
            {
                Idcourse = courseGuid,
                Datetime = (DateTime)(model.Datetime is null ? DateTime.Now : model.Datetime),
                Classroom = model.Classroom,
                Description = model.Description
            });

            await _context.SaveChangesAsync();
            return Ok("Criado aqui");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
