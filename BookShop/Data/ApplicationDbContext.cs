using BookShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookShop.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ApplicationModel> ApplicationModel { get; set; } = default!;
        public DbSet<JobListingModel> JobListingModel { get; set; } = default!;
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            var keysProperties = modelBuilder.Model.GetEntityTypes()
                .Select(x => x.FindPrimaryKey())
                .SelectMany(x => x.Properties);
            foreach (var property in keysProperties)
            {
                property.ValueGenerated = ValueGenerated.OnAdd;
            }

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Adventure", Description = "So funny" },
                new Category { Id = 2, Name = "Roman", Description = "So romantic" },
                new Category { Id = 3, Name = "Horror", Description = "So scary" },
                new Category { Id = 4, Name = "Science", Description = "So Boring" }
               );
			modelBuilder.Entity<Book>().HasData(
				new Book
				{
					Id = 1,
					Title = "C# Programming",
					Description = "Hello",
					Author = "Microsoft",
					Price = 10,
					CategoryId = 1
				},
				new Book
				{
					Id = 2,
					Title = "Advanced Programming",
					Description = "Learning Harder",
					Author = "BTEC",
					Price = 11,
					CategoryId = 2
				},
				new Book
				{
					Id = 3,
					Title = "Java Programming",
					Description = "Basic language",
					Author = "Sun",
					Price = 15,
					CategoryId = 3
				},
				new Book
				{
					Id = 4,
					Title = "Data Structures",
					Description = "Really not easy",
					Author = "Greenwich",
					Price = 20,
					CategoryId = 1
				},
				new Book
				{
					Id = 5,
					Title = "App Dev",
					Description = "Now",
					Author = "Microsoft",
					Price = 10,
					CategoryId = 2
				}
			);

		}
    }
}
