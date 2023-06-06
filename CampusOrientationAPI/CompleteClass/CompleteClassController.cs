using CampusOrientationAPI.Data;
using CampusOrientationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CampusOrientationAPI.CompleteClasses;

[ApiController]
[Route("api/completeclass")]
public class CompleteClassController : ControllerBase
{
    private readonly CampusorientationContext _context;

    public CompleteClassController(CampusorientationContext context)
    {
        this._context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllClassesAsync()
    {
        var classes = await _context.Classes.AsNoTracking().ToListAsync();

        return classes == null ? NotFound() : Ok(classes);
    }

    //[HttpPost]
    //public async Task<IActionResult> PostAsync([FromBody] Completeclass model)
    //{
    //    if (!ModelState.IsValid) return BadRequest();

    //    try
    //    {
    //        await _context.Completeclasses.AddAsync(model);
    //        await _context.SaveChangesAsync();
    //        return Ok("Criado aqui");
    //    }
    //    catch (Exception e)
    //    {
    //        return BadRequest(e.Message);
    //    }
    //}
}
