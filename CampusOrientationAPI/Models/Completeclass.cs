using System;
using System.Collections.Generic;

namespace CampusOrientationAPI.Models;

public partial class Completeclass
{
    public DateTime? Datetimestart { get; set; }

    public DateTime? Datetimeend { get; set; }

    public string? Classroom { get; set; }

    public string? Coursename { get; set; }

    public string? Description { get; set; }

    public string? Nameteacher { get; set; }
}
