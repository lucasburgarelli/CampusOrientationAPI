using System.Diagnostics.CodeAnalysis;

namespace CampusOrientationAPI.Class;

public sealed class Class
{
    [NotNull]
    public String? IdCourse { get; set; }
    [NotNull]
    public DateTime? Datetime { get; set; }
    public String? Description { get; set; }
    public String? Classroom { get; set; }
}
