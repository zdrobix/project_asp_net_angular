using Project.API.Models.Domain;

namespace Project.API.Repositories.Interface
{
	public interface IBlogPostRepository
	{
		Task<BlogPost> CreateAsync(BlogPost blogPost);
	}
}
