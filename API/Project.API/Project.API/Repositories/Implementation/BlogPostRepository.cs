using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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

		public async Task<IEnumerable<BlogPost>> GetAllAsync() =>
			await dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();

		public async Task<BlogPost?> GetByIdAsync(Guid id) =>
			await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);

		public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
		{
			var oldBlogpost = await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
			if (oldBlogpost == null)
				return null;

			dbContext.Entry(oldBlogpost).CurrentValues.SetValues(blogPost);
			oldBlogpost.Categories = blogPost.Categories;
			await dbContext.SaveChangesAsync();
			return blogPost;
		}

		public async Task<BlogPost?> DeleteAsync(Guid id)
		{
			var blogpost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
			if (blogpost == null) 
				return null;

			dbContext.BlogPosts.Remove(blogpost);
			await dbContext.SaveChangesAsync();

			return blogpost;
		}
	}
}
