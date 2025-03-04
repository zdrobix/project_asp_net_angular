using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.API.Data;
using Project.API.Models.Domain;
using Project.API.Models.DTO;
using Project.API.Repositories.Interface;

namespace Project.API.Controllers
{
	// https://localhost:5001/api/categories
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryRepository categoryRepository;

		public CategoriesController(ICategoryRepository categoryRepository)
        {
			this.categoryRepository = categoryRepository;
		}

        //
        [HttpPost] 
		public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
		{
			// dto -> domain model
			var category = new Category
			{
				Name = request.Name,
				UrlHandle = request.UrlHandle
			};

			await categoryRepository.CreateAsync(category);

			// domain model -> dto
			var response = new CategoryDto
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandle = category.UrlHandle
			};

			return Ok(response);
		}

		// GET: https://localhost:7179/api/Categories
		[HttpGet]
		public async Task<IActionResult> GetCategories()
		{
			var categories = await categoryRepository.GetAllAsync();

			// domain model -> dto
			var response = categories.Select(category => new CategoryDto
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandle = category.UrlHandle
			});

			return Ok(response);
		}
	}
}
