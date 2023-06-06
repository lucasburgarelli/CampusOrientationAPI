using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CampusOrientationAPI.CompleteClasses;

[Table("CompleteClass")]
public sealed class CompleteClass
{
    [Column("Datetime")]
    [NotNull]
    public DateTime? Datetime { get; set; }
    [Column("Classroom")]
    [NotNull]
    public String? Classroom { get; set; }
    [Column("CourseName")]
    [NotNull]
    public String? CourseName { get; set; }
    [Column("Description")]
    public String? Description { get; set; }
    [Column("NameTeacher")]
    [NotNull]
    public String? NameTeacher { get; set; }
}
