using Microsoft.EntityFrameworkCore;
using Project.API.Data;
using Project.API.Models.Domain;
using Project.API.Repositories.Interface;

namespace Project.API.Repositories.Implementation
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext dbContext;

		public CategoryRepository(ApplicationDbContext dbContext) =>
			this.dbContext = dbContext;
        public async Task<Category> CreateAsync(Category category)
		{
			await dbContext.Categories.AddAsync(category);
			await dbContext.SaveChangesAsync();

			return category;
		}

		public async Task<IEnumerable<Category>> GetAllAsync() => 
			await dbContext.Categories.ToListAsync();

		public async Task<Category?> GetById(Guid id) =>
			await dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id);

		public async Task<Category?> UpdateAsync(Category category)
		{
			var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

			if (existingCategory != null)
			{
				dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
				await dbContext.SaveChangesAsync();
				return category;
			}

			return null;
		}

		public async Task<Category?> DeleteAsync(Guid id)
		{
			var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id);

			if (existingCategory == null)
				return null;

			dbContext.Categories.Remove(existingCategory);
			await dbContext.SaveChangesAsync();

			return existingCategory;
		}
	}
}
