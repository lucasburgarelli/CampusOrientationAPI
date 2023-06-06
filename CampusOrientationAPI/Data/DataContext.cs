using CampusOrientationAPI.CompleteClasses;
using CampusOrientationAPI.People;
using Microsoft.EntityFrameworkCore;

namespace CampusOrientationAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) {   }

    public DbSet<CompleteClass> Classes { get; set; }
    public DbSet<Person> People { get; set; }
}
