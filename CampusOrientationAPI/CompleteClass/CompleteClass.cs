using System.Diagnostics.CodeAnalysis;

namespace CampusOrientationAPI.CompleteClasses;

public sealed class CompleteClass
{
    [NotNull]
    public DateTime? Datetime { get; set; }
    [NotNull]
    public String? Classroom { get; set; }
    [NotNull]
    public String? CourseName { get; set; }
    public String? Description { get; set; }
    [NotNull]
    public String? NameTeacher { get; set; }
}
