using Microsoft.EntityFrameworkCore;
using SqlOfTheDead.Models;

namespace SqlOfTheDead.Routes;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<ZombieTable> ZombieTable { get; set; }
    public DbSet<ZombieField> ZombieField { get; set; }
    public DbSet<ZombieIndex> ZombieIndex { get; set; }
    public DbSet<ZombieIndexField> ZombieIndexField { get; set; }
}
