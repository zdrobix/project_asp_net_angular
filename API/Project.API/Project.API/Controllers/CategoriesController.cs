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

		public CategoriesController(ICategoryRepository categoryRepository) =>
			this.categoryRepository = categoryRepository;

        // POST 
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

		// GET: https://localhost:7179/api/categories/{id}
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetCategoryById([FromRoute]Guid id) 
		{
			var existingCategory = await categoryRepository.GetById(id);

			return existingCategory == null ? 
				NotFound() : Ok(new CategoryDto
								{
									Id = existingCategory.Id,
									Name = existingCategory.Name,
									UrlHandle = existingCategory.UrlHandle
								});
		}

		// PUT : https://localhost:7179/api/categories/{id}
		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, UpdateCategoryRequestDto request)
		{
			var category = new Category
			{
				Id = id,
				Name = request.Name,
				UrlHandle = request.UrlHandle
			};

			category = await categoryRepository.UpdateAsync(category);

			if (category == null)
				return NotFound();

			// domain model -> dto
			return Ok(new CategoryDto
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandle = category.UrlHandle
			});
		}

		// DELETE : https://localhost:7179/api/categories/{id}
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
		{
			var category = await categoryRepository.DeleteAsync(id);

			return category == null ? NotFound() : Ok(new CategoryDto
														{
															Id = category.Id,
															Name = category.Name,
															UrlHandle = category.UrlHandle
														});
		}
	}
}
