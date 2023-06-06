using System.Diagnostics.CodeAnalysis;

namespace CampusOrientationAPI.Courses;

public sealed class Course
{
    [NotNull]
    public String? Id { get; set; }
    [NotNull]
    public String? Name { get; set; }
    [NotNull]
    public String? RaTeacher { get; set; }
    public String? Description { get; set; }
}
