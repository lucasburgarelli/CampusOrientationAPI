using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CampusOrientationAPI.Courses;

[Table("Course")]
public sealed class Course
{
    [Column("Id")]
    [NotNull]
    public String? Id { get; set; }
    [Column("Name")]
    [NotNull]
    public String? Name { get; set; }
    [Column("RaTeacher")]
    [NotNull]
    public String? RaTeacher { get; set; }
    [Column("Description")]
    public String? Description { get; set; }
}
