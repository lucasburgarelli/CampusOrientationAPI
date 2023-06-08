using CampusOrientationAPI.Data;
using CampusOrientationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusOrientationAPI.Classes;

[ApiController]
[Route("api/class")]
public sealed class ClassController : ControllerBase
{
    private readonly CampusOrientationDBContext _context;

    public ClassController(CampusOrientationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] ClassEssentialViewModel model)
    {
        if (model is null || !ModelState.IsValid) return BadRequest("Login empty or invalid");

        var class_ = await _context.Classes.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Idcourse == model.IdCourse && c.Datetime == model.DateTime);

        return class_ == null ? NotFound() : Ok(class_);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] ClassViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        try
        {
            var class_ = new Class
            {
                Idcourse = model.IdCourse ,
                Datetime = model.DateTime,
                Classroom = model.Classroom,
                Description = model.Description
            };
            await _context.Classes.AddAsync(class_);
            await _context.SaveChangesAsync();
            return Ok("Class add suceded");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromBody] ClassEssentialViewModel model)
    {
        var class_ = await _context.Classes
            .FirstOrDefaultAsync(c => c.Idcourse == model.IdCourse && c.Datetime == model.DateTime);

        try
        {
            if (class_ is null) return Ok();
            _context.Classes.Remove(class_);
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch
        {
            return BadRequest("Verify datetime format");
        }
    }
}
