using System;
using System.Collections.Generic;

namespace CampusOrientationAPI.Models;

public partial class Person
{
    public string Ra { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Password { get; set; }

    public virtual ICollection<Course> Idcourses { get; set; } = new List<Course>();
}
