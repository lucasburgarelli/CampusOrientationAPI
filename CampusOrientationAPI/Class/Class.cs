using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CampusOrientationAPI.Class;

[Table("Class")]
public sealed class Class
{
    [Column("IdCourse")]
    [NotNull]
    public String? IdCourse { get; set; }
    [Column("Datetime")]
    [NotNull]
    public DateTime? Datetime { get; set; }
    [Column("Description")]
    public String? Description { get; set; }
    [Column("Classroom")]
    public String? Classroom { get; set; }
}
