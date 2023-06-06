using Microsoft.AspNetCore.Mvc;

namespace CampusOrientationAPI.Person;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    // GET: api/<PersonController>
    [HttpGet]
    public IEnumerable<string> Get()
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

    // DELETE api/<PersonController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
