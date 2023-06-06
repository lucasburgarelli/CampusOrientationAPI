using System;
using System.Collections.Generic;

namespace CampusOrientationAPI.Models;

public partial class Backupperson
{
    public string Ra { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime Backuptime { get; set; }

    public string Currentuser { get; set; } = null!;
}
