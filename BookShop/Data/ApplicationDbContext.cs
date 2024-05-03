using BookShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookShop.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Book> Books { get; set; }
        public DbSet<ApplicationModel> ApplicationModel { get; set; }
        public DbSet<JobListingModel> JobListingModel { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
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
                new Category { Id = 1, Name = "Back-End Developer", Description = "A Back End Developer is responsible for server-side application logic and integration of the work front-end developers do."},
                new Category { Id = 2, Name = "Front-End Developer", Description = "A Front End Developer is focused on the user interface and user experience of a website or application." },
                new Category { Id = 3, Name = "Full Stack Developer", Description = "A Full Stack Developer is capable of working on both the front-end and back-end portions of an application." },
                new Category { Id = 4, Name = "Mobile Apps Developer", Description = "A Mobile Apps Developer is specialized in creating applications for mobile devices, such as smartphones and tablets." }
               );
			modelBuilder.Entity<JobListingModel>().HasData(
				new JobListingModel
                {
                    JobListingId = 1,
					Title = "C# Programming",
					Description = "Hello",
                    ApplicationDeadline = DateTime.UtcNow.Date.AddDays(-5),
                    Location = "NY",
                    CategoryId = 1,
                },
				new JobListingModel
                {
                    JobListingId = 2,
					Title = "Advanced Programming",
					Description = "Learning Harder",
                    ApplicationDeadline = DateTime.UtcNow.Date.AddDays(-5),
                    Location = "NY",
                    CategoryId = 2,
                },
				new JobListingModel
                {
                    JobListingId = 3,
					Title = "Java Programming",
					Description = "Basic language",
                    ApplicationDeadline = DateTime.UtcNow.Date.AddDays(-5),       
                    Location = "NY",
                    CategoryId = 3  
                },
				new JobListingModel
                {
                    JobListingId = 4,
					Title = "Data Structures",
					Description = "Really not easy",
                    ApplicationDeadline = DateTime.UtcNow.Date.AddDays(-5),
                    Location = "NY",
                    CategoryId = 4
                }
			);

		}
    }
}
