using Microsoft.EntityFrameworkCore;

namespace pickql;

internal class ElementDbContext : DbContext {

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        new LegoElementConfiguration().Configure(modelBuilder.Entity<LegoElement>());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

        // IDK how to get this to work, whatever
        //options.UseSqlite(Config.GetConnectionString("LegoPABDb"));

        optionsBuilder.UseSqlite("Data Source=pab-1697316613-db.sqlite3");
    }


    public DbSet<LegoElement> Elements { get; set; }

}