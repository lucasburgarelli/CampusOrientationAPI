using System.ComponentModel.DataAnnotations;

namespace CampusOrientationAPI.Classes;

public sealed class ClassViewModel
{
    [Required]
    public String IdCourse { get; set; }
    [Required]
    public DateTime DateTime { get; set; }
    [Required]
    public String Classroom { get; set; }
    [Required]
    public String Description { get; set; }
}