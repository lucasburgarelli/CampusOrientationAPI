﻿using CampusOrientationAPI.CompleteClasses;
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

    [HttpGet("ra")]
    public async Task<IActionResult> GetClassesTodayByUserAsync([FromQuery] CompleteClassRaViewModel model)
    {
        // TODO select with 
        var classes = await _context.Classes
            .Include(c => c.IdcourseNavigation)
            .ThenInclude(c => c.Idstudents)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.IdcourseNavigation.Idstudents.FirstOrDefault(s => s.Ra == model.Ra) != null
            && c.Datetime.ToUniversalTime().Day == DateTime.Now.ToUniversalTime().Day);

        var classesSelected = await _context.Completeclasses
            .FirstOrDefaultAsync(c => c.Datetime.Value.ToUniversalTime().Day == DateTime.Now.ToUniversalTime().Day);

        return classes == null ? NotFound() : Ok(classes);
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
                Datetime = (DateTime)(model.Datetime is null ? DateTime.Now : model.Datetime),
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
