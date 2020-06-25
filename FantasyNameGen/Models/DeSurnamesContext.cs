using Microsoft.EntityFrameworkCore;

namespace FantasyNameGen.Models
{
    public class DeSurnamesContext : DbContext
    {
        public DbSet<DeSurname> DeSurnames { get; set; }
        public DeSurnamesContext(DbContextOptions<DeSurnamesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}