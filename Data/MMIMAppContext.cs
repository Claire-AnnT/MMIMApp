using MMIMApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MMIMApp.Data
{
    class MMIMAppContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=localdb.db; Foreign Keys=True;");
        }

        public static void SeedTestData()
        {
            using var context = new MMIMAppContext();


            if (!context.Categories.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    context.Categories.Add(new Category($"Category {i}"));
                }
                context.SaveChanges();
            }

            var categories = context.Categories.ToList();

            if (!context.Products.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    context.Products.Add(new Product(
                        $"Product {i}",
                        $"Brand {i}",
                        10.5m + i,
                        5 + i,
                        2 + i,
                        categories[i % categories.Count]
                    ));
                }

                context.SaveChanges();
            }
        }
    }
}
