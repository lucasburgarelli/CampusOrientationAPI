using CampusOrientationAPI.Data;
using CampusOrientationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusOrientationAPI.People;

[ApiController]
[Route("api/person")]
public sealed class PersonController : ControllerBase
{
    private readonly CampusOrientationDBContext _context;

    public PersonController(CampusOrientationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPersonLoginAsync([FromQuery] LoginViewModel model)
    {
        if(model is null || !ModelState.IsValid) return BadRequest("Login empty or invalid");

        var person = await _context.People.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Ra == model.Ra && p.Password == model.Password);

        return Ok(person);
    }

    [HttpGet("ra")]
    public async Task<IActionResult> GetPersonByRaAsync([FromQuery] String ra)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid entry");

        var person = await _context.People.AsNoTracking().FirstOrDefaultAsync(p => p.Ra == ra);

        return Ok(person);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] PersonAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        try
        {
            await _context.People.AddAsync(new Person
            {
                Name = model.Name,
                Password = model.Password
            });
            await _context.SaveChangesAsync();
            return Ok("Person add suceded");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
