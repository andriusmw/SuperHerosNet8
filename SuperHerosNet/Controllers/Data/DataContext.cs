using Microsoft.EntityFrameworkCore;
using SuperHerosNet.Controllers.Entities;

namespace SuperHerosNet.Controllers.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
