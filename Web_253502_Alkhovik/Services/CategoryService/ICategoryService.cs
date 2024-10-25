using Web_253502_Alkhovik.Domain.Models;
using Web_253502_Alkhovik.Domain.Entities;

namespace Web_253502_Alkhovik.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}