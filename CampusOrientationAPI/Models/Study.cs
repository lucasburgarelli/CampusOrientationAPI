namespace CampusOrientationAPI.Models;

public sealed class Study
{
    public string IdStudent { get; set; }
    public string IdCourse { get; set; }

    public Course Course { get; set; }
    public Person Student { get; set; }
}
