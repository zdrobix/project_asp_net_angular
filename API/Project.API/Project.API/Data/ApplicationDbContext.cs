using Microsoft.EntityFrameworkCore;
using Project.API.Models.Domain;

namespace Project.API.Data
{
	public class ApplicationDbContext : DbContext
	{
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}
