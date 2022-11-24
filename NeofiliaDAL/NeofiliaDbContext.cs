using Microsoft.EntityFrameworkCore;
using Neofilia.DAL.Models;

namespace Neofilia.DAL
{
    public class NeofiliaDbContext : DbContext
    {
        public NeofiliaDbContext()
        {
        }
        public NeofiliaDbContext(DbContextOptions<NeofiliaDbContext> options) : base(options) { }

        public DbSet<Question> Question { get; set; }
    }
}
