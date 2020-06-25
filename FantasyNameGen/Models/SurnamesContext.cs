using Microsoft.EntityFrameworkCore;

namespace FantasyNameGen.Models
{
    public class SurnamesContext : DbContext
    {
        public DbSet<Surname> Surnames { get; set; }
        public SurnamesContext(DbContextOptions<SurnamesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
