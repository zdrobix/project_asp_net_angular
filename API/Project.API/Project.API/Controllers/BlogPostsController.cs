using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.API.Models.Domain;
using Project.API.Models.DTO;
using Project.API.Repositories.Interface;

namespace Project.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostsController : ControllerBase
	{
		private readonly IBlogPostRepository blogPostRepository;

		public BlogPostsController(IBlogPostRepository blogPostRepository)
		{
			this.blogPostRepository = blogPostRepository;
		}
		// POST: https://localhost:7179/api/blogposts
		[HttpPost]
		public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
		{
			// dto -> domain model
			var blogPost = new BlogPost
			{
				Title = request.Title,
				Content = request.Content,
				FeatureImageUrl = request.FeatureImageUrl,
				PublishedDate = request.PublishedDate,
				UrlHandle = request.UrlHandle,
				Author = request.Author,
				IsVisible = request.IsVisible,
				ShortDescription = request.ShortDescription
			};

			blogPost = await blogPostRepository.CreateAsync(blogPost);

			// domain model -> dto
			return Ok(new BlogPostDto
			{
				Id = blogPost.Id,
				Author = blogPost.Author,
				Content = blogPost.Content,
				PublishedDate = blogPost.PublishedDate,
				FeatureImageUrl = blogPost.FeatureImageUrl,
				IsVisible = blogPost.IsVisible,
				Title = blogPost.Title,
				ShortDescription = blogPost.ShortDescription,
				UrlHandle = blogPost.UrlHandle,
			});
		}
	}
}
