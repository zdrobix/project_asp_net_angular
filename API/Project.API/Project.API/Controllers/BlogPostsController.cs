using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.API.Models.Domain;
using Project.API.Models.DTO;
using Project.API.Repositories.Implementation;
using Project.API.Repositories.Interface;
using System.Security.Cryptography.Xml;

namespace Project.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostsController : ControllerBase
	{
		private readonly IBlogPostRepository blogPostRepository;
		private readonly ICategoryRepository categoryRepository;

		public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
		{
			this.blogPostRepository = blogPostRepository;
			this.categoryRepository = categoryRepository;
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
				FeaturedImageUrl = request.FeaturedImageUrl,
				PublishedDate = request.PublishedDate,
				UrlHandle = request.UrlHandle,
				Author = request.Author,
				IsVisible = request.IsVisible,
				ShortDescription = request.ShortDescription,
				Categories = new List<Category>()
			};

			foreach (var guid in request.Categories)
			{
				var category = await categoryRepository.GetById(guid);
				if (category != null)
				{
					blogPost.Categories.Add(category);
				}
			}

			blogPost = await blogPostRepository.CreateAsync(blogPost);

			// domain model -> dto
			return Ok(new BlogPostDto
			{
				Id = blogPost.Id,
				Author = blogPost.Author,
				Content = blogPost.Content,
				PublishedDate = blogPost.PublishedDate,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				IsVisible = blogPost.IsVisible,
				Title = blogPost.Title,
				ShortDescription = blogPost.ShortDescription,
				UrlHandle = blogPost.UrlHandle,
				Categories = blogPost.Categories.Select(category =>
					new CategoryDto
					{
						Id = category.Id,
						Name = category.Name,
						UrlHandle = category.UrlHandle
					}).ToList()
			});
		}

		// GET: https://localhost:7179/api/blogposts
		[HttpGet]
		public async Task<IActionResult> GetAllBlogPosts()
		{
			var blogposts = await blogPostRepository.GetAllAsync();

			// domain model -> dto
			var response = new List<BlogPostDto>();
			foreach (var blogpost in blogposts)
			{
				response.Add( new BlogPostDto
					{
						Id = blogpost.Id,
						Author = blogpost.Author,
						Content = blogpost.Content,
						PublishedDate = blogpost.PublishedDate,
						FeaturedImageUrl = blogpost.FeaturedImageUrl,
						IsVisible = blogpost.IsVisible,
						Title = blogpost.Title,
						ShortDescription = blogpost.ShortDescription,
						UrlHandle = blogpost.UrlHandle,
						Categories = blogpost.Categories.Select(category =>
							new CategoryDto
							{
								Id = category.Id,
								Name = category.Name,
								UrlHandle = category.UrlHandle
							}).ToList()
					}
				);
			}
			return Ok(response);
		}


		// GET: https://localhost:7179/api/blogposts{id}
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetBlogPostById([FromRoute]Guid id)
		{
			var blogPost = await blogPostRepository.GetByIdAsync(id);
			if (blogPost == null)
				return NotFound();
			return Ok(new BlogPostDto
			{
				Id = blogPost.Id,
				Author = blogPost.Author,
				Content = blogPost.Content,
				PublishedDate = blogPost.PublishedDate,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				IsVisible = blogPost.IsVisible,
				Title = blogPost.Title,
				ShortDescription = blogPost.ShortDescription,
				UrlHandle = blogPost.UrlHandle,
				Categories = blogPost.Categories.Select(category =>
					new CategoryDto
					{
						Id = category.Id,
						Name = category.Name,
						UrlHandle = category.UrlHandle
					}).ToList()
			});
		}

		// PUT: https://localhost:7179/api/blogposts/{id}
		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> UpdateBlogPostById([FromRoute]Guid id, UpdateBlogPostRequestDto request)
		{
			// dto -> domain model
			var blogpost = new BlogPost
			{
				Id = id,
				Title = request.Title,
				Content = request.Content,
				FeaturedImageUrl = request.FeaturedImageUrl,
				PublishedDate = request.PublishedDate,
				UrlHandle = request.UrlHandle,
				Author = request.Author,
				IsVisible = request.IsVisible,
				ShortDescription = request.ShortDescription,
				Categories = new List<Category>()
			};

			foreach (var categoryGuid in request.Categories)
			{
				var category = await categoryRepository.GetById(categoryGuid);
				if (category != null) 
					blogpost.Categories.Add(category);
			}

			var updatedBlogpost = await blogPostRepository.UpdateAsync(blogpost);
			if (updatedBlogpost == null)
				return NotFound();

			return Ok(
				new BlogPostDto
				{
					Id = blogpost.Id,
					Author = blogpost.Author,
					Content = blogpost.Content,
					PublishedDate = blogpost.PublishedDate,
					FeaturedImageUrl = blogpost.FeaturedImageUrl,
					IsVisible = blogpost.IsVisible,
					Title = blogpost.Title,
					ShortDescription = blogpost.ShortDescription,
					UrlHandle = blogpost.UrlHandle,
					Categories = blogpost.Categories.Select(category =>
						new CategoryDto
						{
							Id = category.Id,
							Name = category.Name,
							UrlHandle = category.UrlHandle
						}).ToList()
				}
			);
		}

		// DELETE: https://localhost:7179/api/blogposts/{id}
		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> DeleteBlogPost([FromRoute]Guid id)
		{
			var blogpost = await blogPostRepository.DeleteAsync(id);
			if (blogpost == null)
				return NotFound();
			return Ok(
					new BlogPostDto
					{
						Id = blogpost.Id,
						Author = blogpost.Author,
						Content = blogpost.Content,
						PublishedDate = blogpost.PublishedDate,
						FeaturedImageUrl = blogpost.FeaturedImageUrl,
						IsVisible = blogpost.IsVisible,
						Title = blogpost.Title,
						ShortDescription = blogpost.ShortDescription,
						UrlHandle = blogpost.UrlHandle
					}
			);
		}
	}
}
