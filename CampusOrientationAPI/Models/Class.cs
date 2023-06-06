using System;
using System.Collections.Generic;

namespace CampusOrientationAPI.Models;

public partial class Class
{
    public string Idcourse { get; set; } = null!;

    public DateTime Datetime { get; set; }

    public string? Classroom { get; set; }

    public string? Description { get; set; }

    public virtual Course IdcourseNavigation { get; set; } = null!;
}
