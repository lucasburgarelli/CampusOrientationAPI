using System.ComponentModel.DataAnnotations;

namespace CampusOrientationAPI.Courses;

public class CourseViewModel
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Description { get; set; }
    [Required]
    public string Idteacher { get; set; }
}
