using System.ComponentModel.DataAnnotations;

namespace CampusOrientationAPI.Classes;

public class ClassEssentialViewModel
{
    [Required]
    public String IdCourse { get; set; }
    [Required]
    public DateTime DateTimeStart { get; set; }
    public DateTime DateTimeEnd { get; set; }
}
