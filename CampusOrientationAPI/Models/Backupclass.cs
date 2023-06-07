using System;
using System.Collections.Generic;

namespace CampusOrientationAPI.Models;

public sealed class Backupclass
{
    public string Idcourse { get; set; } = null!;

    public DateTime Datetime { get; set; }

    public string? Classroom { get; set; }

    public string? Description { get; set; }

    public DateTime Backuptime { get; set; }

    public string Currentuser { get; set; } = null!;
}
