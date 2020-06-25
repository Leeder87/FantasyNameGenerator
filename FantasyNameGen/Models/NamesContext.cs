using Microsoft.EntityFrameworkCore;

namespace FantasyNameGen.Models
{
    public class NamesContext : DbContext
    {
        public DbSet<Name> Names { get; set; }
        public NamesContext(DbContextOptions<NamesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
