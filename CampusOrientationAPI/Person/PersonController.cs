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
    public async Task<IActionResult> GetPersonLoginAsync([FromBody] LoginViewModel model)
    {
        if(model is null || !ModelState.IsValid) return BadRequest("Login empty or invalid");

        var person = await _context.People.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Ra == model.Ra && p.Password == model.Password);

        return person == null ? NotFound() : Ok(person);
    }

    [HttpGet("ra")]
    public async Task<IActionResult> GetPersonByRaAsync([FromBody] String ra)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid entry");

        var person = await _context.People.AsNoTracking().FirstOrDefaultAsync(p => p.Ra == ra);

        return person == null ? NotFound() : Ok(person);
    }

    [HttpPost]
    public async Task<IActionResult> PostPersonAsync([FromBody] Person model)
    {
        if (!ModelState.IsValid) return BadRequest();

        try
        {
            model.Ra = Guid.NewGuid().ToString();

            await _context.People.AddAsync(model);
            await _context.SaveChangesAsync();
            return Ok("Person add suceded");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
