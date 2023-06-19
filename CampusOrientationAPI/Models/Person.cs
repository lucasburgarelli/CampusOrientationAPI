using System;
using System.Collections.Generic;

namespace CampusOrientationAPI.Models;

public partial class Person
{
    public string Id { get; set; } = null!;

    public string Ra { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Password { get; set; }

    public IList<Study> Studies { get; set; }
    public IList<Course> Courses { get; set; }
}
