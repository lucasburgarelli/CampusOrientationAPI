using System;
using System.Collections.Generic;

namespace CampusOrientationAPI.Models;

public partial class Course
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Rateacher { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Person> Rastudents { get; set; } = new List<Person>();
}
