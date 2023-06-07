using CampusOrientationAPI.Data;
using CampusOrientationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusOrientationAPI.Classes;

[Route("api/class")]
[ApiController]
public sealed class ClassController : ControllerBase
{
    private readonly CampusOrientationDBContext _context;

    public ClassController(CampusOrientationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetClassAsync([FromBody] ClassEssentialViewModel model)
    {
        if (model is null || !ModelState.IsValid) return BadRequest("Login empty or invalid");

        var class_ = await _context.Classes.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Idcourse == model.IdCourse && c.Datetime == model.DateTime);

        return class_ == null ? NotFound() : Ok(class_);
    }

    [HttpPost]
    public async Task<IActionResult> PostClassAsync([FromBody] Class model)
    {
        if (!ModelState.IsValid) return BadRequest();

        try
        {
            await _context.Classes.AddAsync(model);
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
            return BadRequest();
        }
    }
}
