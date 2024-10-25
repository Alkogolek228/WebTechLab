using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Domain.Models;

namespace Web_253502_Alkhovik.API.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}