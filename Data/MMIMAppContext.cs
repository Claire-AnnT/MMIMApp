using MMIMApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MMIMApp.Data
{
    class MMIMAppContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Report> Reports { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=localdb.db; Foreign Keys=True; MaxPoolSize=16;");
        }
    }
}
