using Exercise002_EfCore_Better_Configure.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise002_EfCore_Better_Configure;
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(builder =>
        {

            builder.ToTable("Companies");
            builder.HasData(new Company
            {
                Id = 1,
                Name = "Awesome Company"
            });
        });
    }
}
