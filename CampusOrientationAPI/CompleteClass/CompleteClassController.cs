using CampusOrientationAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CampusOrientationAPI.CompleteClasses;

[Route("api/[controller]")]
[ApiController]
public class CompleteClassController : ControllerBase
{
    private readonly DataContext _context;

    public CompleteClassController(DataContext context)
    {
        this._context = context;
    }
    public async Task<IActionResult> GetAllClassesAsync()
    {
        return new string[] { "value1", "value2" };
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }


    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
