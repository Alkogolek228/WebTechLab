using Microsoft.EntityFrameworkCore;
using Web_253502_Alkhovik.API.Data;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Domain.Models;

namespace Web_253502_Alkhovik.API.Services.CategoryService;

public class CategoryService : ICategoryService
{
	private readonly AppDbContext _context;
	public CategoryService(AppDbContext context)
	{
		_context = context;
	}

	public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
	{
		var categories = await _context.Categories.ToListAsync();
		if (!categories.Any() || categories is null)
		{
			return ResponseData<List<Category>>.Error("No categories in db");
		}

		return ResponseData<List<Category>>.Success(categories);
	}
}