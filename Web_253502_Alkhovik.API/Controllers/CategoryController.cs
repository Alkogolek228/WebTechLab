using Microsoft.AspNetCore.Mvc;
using Web_253502_Alkhovik.API.Services.CategoryService;

namespace Web_253502_Alkhovik.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        return Ok(await _categoryService.GetCategoryListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int? id)
    {
		throw new NotImplementedException();
	}
    [HttpPost]
    public IActionResult Create()
    {
		throw new NotImplementedException();
	}

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Edit(int? id)
    {
		throw new NotImplementedException();
	}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int? id)
    {
        throw new NotImplementedException();
    }
}