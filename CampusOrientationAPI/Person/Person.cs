using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CampusOrientationAPI.People;

[Table("Person")]
public sealed class Person
{
    [Column("Ra")]
    [NotNull]
    public String? Ra { get; set; }
    [NotNull]
    public String? Name { get; set; }
    [Column("Password")]
    public String? Password { get; set; }
}
