using System.ComponentModel.DataAnnotations;

namespace CampusOrientationAPI.Classes;

public class ClassEssentialViewModel
{
    [Required]
    public String IdCourse { get; set; }
    [Required]
    public DateTime DateTime { get; set; }
}
