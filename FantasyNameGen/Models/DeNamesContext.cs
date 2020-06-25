using Microsoft.EntityFrameworkCore;

namespace FantasyNameGen.Models
{
    public class DeNamesContext : DbContext
    {
        public DbSet<DeName> DeNames { get; set; }
        public DeNamesContext(DbContextOptions<DeNamesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}