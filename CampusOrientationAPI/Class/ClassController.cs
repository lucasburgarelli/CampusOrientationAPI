﻿using Microsoft.AspNetCore.Mvc;

namespace CampusOrientationAPI.Class;

[Route("api/[controller]")]
[ApiController]
public class ClassController : ControllerBase
{
    // GET: api/<ClassController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<ClassController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ClassController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ClassController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ClassController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}