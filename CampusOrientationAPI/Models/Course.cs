using System;
using System.Collections.Generic;

namespace CampusOrientationAPI.Models;

public partial class Course
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Idteacher { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Person? IdteacherNavigation { get; set; }

    public virtual ICollection<Person> Idstudents { get; set; } = new List<Person>();
}
