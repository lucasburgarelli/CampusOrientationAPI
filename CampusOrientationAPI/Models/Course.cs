using System;
using System.Collections.Generic;

namespace CampusOrientationAPI.Models;

public partial class Course
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Idteacher { get; set; }

    public virtual Person? Teacher { get; set; }
    public IList<Class> Classes { get; set; }

    public IList<Study> Studies { get; set; }
}
