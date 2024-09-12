using Microsoft.EntityFrameworkCore;
using Web_253502_Alkhovik.API.Data;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Domain.Models;

namespace Web_253502_Alkhovik.API.Services.CarService;

public class CarService : ICarService
{
	private readonly int _maxPageSize = 20;
	private readonly AppDbContext _context;
	public CarService(AppDbContext context)
	{
		_context = context;
	}
	public async Task<ResponseData<Car>> CreateProductAsync(Car car, IFormFile? formFile)
	{
		var newProduct = await _context.Cars.AddAsync(car);

		return ResponseData<Car>.Success(newProduct.Entity);
	}

	public async Task DeleteProductAsync(int id)
	{
		var product = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
		if (product is null)
		{
			return;
		}

		_context.Entry(product).State = EntityState.Deleted;
	}

	public async Task<ResponseData<Car>> GetProductByIdAsync(int id)
	{
		var product = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
		if (product is null)
		{
			return ResponseData<Car>.Error($"No such object with id : {id}");
		}
		return ResponseData<Car>.Success(product);
	}

	public async Task<ResponseData<ProductListModel<Car>>> GetProductListAsync(
								string? categoryNormalizedName, 
								int pageNo = 1, 
								int pageSize = 3)
	{
		var query = _context.Cars.AsQueryable();
		var dataList = new ProductListModel<Car>();

		if (pageSize > _maxPageSize)
			pageSize = _maxPageSize;

		query = query
			.Where(d => categoryNormalizedName == null || d.Category.NormalizedName.Equals(categoryNormalizedName));

		var count = await query.CountAsync();
		if (count == 0)
		{
			return ResponseData<ProductListModel<Car>>.Success(dataList);
		}

		int totalPages = (int)Math.Ceiling(count / (double)pageSize);

		if (pageNo > totalPages)
		{
			return ResponseData<ProductListModel<Car>>.Error("No such page");
		}

		dataList.Items = await query
								.OrderBy(c => c.Id)
								.Skip((pageNo - 1) * pageSize)
								.Take(pageSize)
								.ToListAsync();

		dataList.CurrentPage = pageNo;
		dataList.TotalPages = totalPages;

		return ResponseData<ProductListModel<Car>>.Success(dataList);
	}

	public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateProductAsync(int id, Car car, IFormFile? formFile)
	{
		var product = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
		if (product is null)
			return;

		product.Price = car.Price;
		product.Description = car.Description;
		product.Category = car.Category;
		product.CategoryId = car.CategoryId;
		product.Amount = car.Amount;
		product.Image = car.Image;

		_context.Entry(product).State = EntityState.Modified;
	}
}