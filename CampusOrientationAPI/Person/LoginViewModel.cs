using System.ComponentModel.DataAnnotations;

namespace CampusOrientationAPI.People;

public sealed class LoginViewModel
{
    [Required]
    public String Ra { get; set; }
    [Required]
    public String Password { get; set; }
}
