using System.ComponentModel.DataAnnotations;

namespace CampusOrientationAPI.People;

public class PersonAddViewModel
{
    [Required]
    public String Name { get; set; }
    [Required]
    public String Password { get; set; }
}
