using Microsoft.EntityFrameworkCore;

namespace CampusOrientationAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {

    }


}
