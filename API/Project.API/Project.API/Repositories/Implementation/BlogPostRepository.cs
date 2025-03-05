using Microsoft.AspNetCore.Http.HttpResults;
using Project.API.Data;
using Project.API.Models.Domain;
using Project.API.Repositories.Interface;

namespace Project.API.Repositories.Implementation
{
	public class BlogPostRepository : IBlogPostRepository
	{
		private readonly ApplicationDbContext dbContext;

		public BlogPostRepository(ApplicationDbContext dbContext)
        {
			this.dbContext = dbContext;
		}

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
		{
			await dbContext.BlogPosts.AddAsync(blogPost);
			await dbContext.SaveChangesAsync();

			return blogPost;
		}
	}
}
