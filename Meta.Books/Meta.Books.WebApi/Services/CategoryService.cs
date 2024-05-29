using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Repositories.Interfaces;
using Meta.Books.WebApi.Services.Interfaces;

namespace Meta.Books.WebApi.Services;

public class CategoryService : IBaseService<CategoryDto>
{
    private readonly IBaseRepository<Category> _categoryRepository;
    
    public CategoryService(IBaseRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> SaveAsync(CategoryDto categoryDto)
    {
        var category = new Category
        {
            name = categoryDto.name,
            created_by = 0,
            created_date = DateTime.Now,
            updated_by = 0,
            updated_date = DateTime.Now
        };
        
        category = await _categoryRepository.SaveAsync(category);
        categoryDto.id = category.id;

        return categoryDto;
    }

    public async Task<CategoryDto> UpdateAsync(CategoryDto categoryDto)
    {
        var category = await _categoryRepository.GetById(categoryDto.id);

        if (category == null)
            throw new Exception("Category not found");
            
        category.name = categoryDto.name;
        category.updated_by = 0;
        category.updated_date = DateTime.Now;
        
        await _categoryRepository.UpdateAsync(category);

        return categoryDto;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var categoriesDto = categories.Select(c => new CategoryDto(c)).ToList();
        return categoriesDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetById(id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }
        
        return await _categoryRepository.DeleteAsync(id);
    }

    public async Task<CategoryDto> GetById(int id)
    {
        var category = await _categoryRepository.GetById(id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }

        var categoryDto = new CategoryDto(category);
        return categoryDto;
    }
}