﻿using Project.API.Models.Domain;

namespace Project.API.Repositories.Interface
{
	public interface ICategoryRepository
	{
		Task<Category> CreateAsync(Category category);

		Task<IEnumerable<Category>> GetAllAsync();
	}
}
