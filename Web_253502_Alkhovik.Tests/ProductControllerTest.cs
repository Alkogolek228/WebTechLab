using NSubstitute;
using Web_253502_Alkhovik.Domain.Models;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Services.CarService;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics.CodeAnalysis;

namespace Web_253502_Alkhovik.Tests;

public class ProductControllerTest
{
	private readonly ICarService _carService;
	private readonly ICategoryService _categoryService;
	public ProductControllerTest()
	{
		_carService = Substitute.For<ICarService>();
		_categoryService = Substitute.For<ICategoryService>();
	}

	[Fact]
	public void Index_GettingProductListFailed_ShouldReturn404()
	{
		// Arrange
		_carService.GetProductListAsync(null).Returns(new ResponseData<ProductListModel<Car>>()
		{
			Successfull = false
		});

		_categoryService.GetCategoryListAsync().Returns(new ResponseData<List<Category>>()
		{
			Successfull = true
		});

		var controllerContext = new ControllerContext();
		var httpContext = Substitute.For<HttpContext>();
		httpContext.Request.Headers.Returns(new HeaderDictionary());
		controllerContext.HttpContext = httpContext;

		var controller = new ProductController(_carService, _categoryService)
		{
			ControllerContext = controllerContext
		};

		//var header = new Dictionary<string, StringValues>(){ ["x-requested-with"] = "XMLHttpRequest" };
		// Act
		var result = controller.Index(null).Result;

		// Assert
		Assert.IsType<NotFoundObjectResult>(result);
	}	
	
	[Fact]
	public void Index_GettingCategoryListFailed_ShouldReturn404()
	{
		// Arrange
		_carService.GetProductListAsync(null).Returns(new ResponseData<ProductListModel<Car>>()
		{
			Successfull = true
		});

		_categoryService.GetCategoryListAsync().Returns(new ResponseData<List<Category>>()
		{
			Successfull = false
		});

		var controllerContext = new ControllerContext();
		var httpContext = Substitute.For<HttpContext>();
		httpContext.Request.Headers.Returns(new HeaderDictionary());
		controllerContext.HttpContext = httpContext;

		var controller = new ProductController(_carService, _categoryService)
		{
			ControllerContext = controllerContext
		};

		// Act
		var result = controller.Index(null).Result;

		// Assert
		Assert.IsType<NotFoundObjectResult>(result);
	}

	[Fact]
	public void Index_ViewData_Should_Contain_CategoryList()
	{
		// Arrange
		_carService.GetProductListAsync(null).Returns(new ResponseData<ProductListModel<Car>>()
		{
			Successfull = true,
			Data = new ProductListModel<Car>
			{
				Items = GetTestCars()
			}
		});

		_categoryService.GetCategoryListAsync().Returns(new ResponseData<List<Category>>()
		{
			Successfull = true,
			Data = GetTestCategories()
		});

		var controllerContext = new ControllerContext();
		var tempDataProvider = Substitute.For<ITempDataProvider>();
		var httpContext = Substitute.For<HttpContext>();
		httpContext.Request.Headers.Returns(new HeaderDictionary());
		controllerContext.HttpContext = httpContext;

		var controller = new ProductController(_carService, _categoryService)
		{
			ControllerContext = controllerContext,
			TempData = new TempDataDictionary(controllerContext.HttpContext, tempDataProvider)
		};

		// Act
		var result = controller.Index(null).Result;

		// Assert
		Assert.NotNull(result);

		var viewResult = Assert.IsType<ViewResult>(result);

		var categories = viewResult.ViewData["Categories"] as List<Category>;

		Assert.NotNull(categories);
		Assert.NotEmpty(categories);
		Assert.Equal(GetTestCategories(), categories, new CategoryComparer());
	}

	[Fact]
	public void Index_ViewData_Should_Contain_CorrectCurrentCategory()
	{
		// Arrange
		_carService.GetProductListAsync(GetTestCategories()[0].NormalizedName).Returns(new ResponseData<ProductListModel<Car>>()
		{
			Successfull = true,
			Data = new ProductListModel<Car>
			{
				Items = GetTestCars()
			}
		});

		_categoryService.GetCategoryListAsync().Returns(new ResponseData<List<Category>>()
		{
			Successfull = true,
			Data = GetTestCategories()
		});

		var controllerContext = new ControllerContext();
		var tempDataProvider = Substitute.For<ITempDataProvider>();
		var httpContext = Substitute.For<HttpContext>();
		httpContext.Request.Headers.Returns(new HeaderDictionary());
		controllerContext.HttpContext = httpContext;

		var controller = new ProductController(_carService, _categoryService)
		{
			ControllerContext = controllerContext,
			TempData = new TempDataDictionary(controllerContext.HttpContext, tempDataProvider)
		};

		// Act
		var result = controller.Index(GetTestCategories()[0].NormalizedName).Result;

		// Assert
		Assert.NotNull(result);

		var viewResult = Assert.IsType<ViewResult>(result);

		var currentCategory = viewResult.ViewData["CurrentCategory"].ToString();

		Assert.NotNull(currentCategory);
		Assert.NotEmpty(currentCategory);
		Assert.Equal(GetTestCategories()[0].Name, currentCategory);
	}

	[Fact]
	public void Index_View_Should_Contain_ProductList()
	{
		// Arrange
		_carService.GetProductListAsync(null).Returns(new ResponseData<ProductListModel<Car>>()
		{
			Successfull = true,
			Data = new ProductListModel<Car>
			{
				Items = GetTestCars()
			}
		});

		_categoryService.GetCategoryListAsync().Returns(new ResponseData<List<Category>>()
		{
			Successfull = true,
			Data = GetTestCategories()
		});

		var controllerContext = new ControllerContext();
		var tempDataProvider = Substitute.For<ITempDataProvider>();
		var httpContext = Substitute.For<HttpContext>();
		httpContext.Request.Headers.Returns(new HeaderDictionary());
		controllerContext.HttpContext = httpContext;

		var controller = new ProductController(_carService, _categoryService)
		{
			ControllerContext = controllerContext,
			TempData = new TempDataDictionary(controllerContext.HttpContext, tempDataProvider)
		};

		// Act
		var result = controller.Index(null).Result;

		// Assert
		Assert.NotNull(result);

		var viewResult = Assert.IsType<ViewResult>(result);

		var productsList = Assert.IsType<ProductListModel<Car>>(viewResult.Model);

		Assert.NotNull(productsList);
		Assert.NotEmpty(productsList.Items);
		Assert.Equal(GetTestCars(), productsList.Items, new CarComparer());
	}

	private List<Category> GetTestCategories()
	{
		return new List<Category>() {
				new Category() { Id = 1, Name="Name1", NormalizedName="name-1"},
				new Category() { Id = 2, Name="Name2", NormalizedName="name-2"}
			};
	}

	private List<Car> GetTestCars()
	{
		return new List<Car>()
				{
					new Car() { Id = 1, Price=1032.2M, Description="city-1", Amount = 300, CategoryId = 1},
					new Car() { Id = 2, Price=66.2M, Description="city-2", Amount = 400, CategoryId = 2},
				};
	}
}

public class CategoryComparer : IEqualityComparer<Category>
{
	public bool Equals(Category? x, Category? y)
	{
		if (ReferenceEquals(x, y))
			return true;

		if (ReferenceEquals(y, null) || ReferenceEquals(x,null)) 
			return false;

		return x.Name == y.Name && x.NormalizedName == y.NormalizedName;
	}

	public int GetHashCode([DisallowNull] Category obj)
	{
		return obj.Id.GetHashCode() ^ obj.Name.GetHashCode() ^ obj.NormalizedName.GetHashCode();	
	}
}

public class CarComparer : IEqualityComparer<Car>
{
	public bool Equals(Car? x, Car? y)
	{
		if (ReferenceEquals(x, y))
			return true;

		if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
			return false;

		return x.CategoryId == y.CategoryId
			&& x.Description == y.Description
			&& x.Amount == y.Amount
			&& x.Price == y.Price
			&& x.Image == y.Image;
	}

	public int GetHashCode([DisallowNull] Car obj)
	{
		return obj.Id.GetHashCode()
			^ obj.Price.GetHashCode() 
			^ obj.CategoryId.GetHashCode() 
			^ obj.Description.GetHashCode() 
			^ obj.Amount.GetHashCode();
	}
}