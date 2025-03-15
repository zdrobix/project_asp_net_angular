using Project.API.Models.Domain;

namespace Project.API.Repositories.Interface
{
	public interface IBlogPostRepository
	{
		Task<BlogPost> CreateAsync(BlogPost blogPost);
		Task<IEnumerable<BlogPost>> GetAllAsync();
		Task<BlogPost?> GetByIdAsync(Guid id);
	}
}
