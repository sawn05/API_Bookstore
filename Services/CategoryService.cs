using API_Bookstore.Models.DTOs.Category;
using API_Bookstore.Models.Entities;
using API_Bookstore.Repositories;
using API_Bookstore.Repositories.Interfaces;
using API_Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API_Bookstore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDTO> CreateAsync(CreateCategoryDTO categoryCreateDTO)
        {
            if (await _categoryRepository.IsCategoryNameExistsAsync(categoryCreateDTO.Name))
            {
                throw new InvalidOperationException($"A category with the name '{categoryCreateDTO.Name}' already exists.");
            }

            // Nếu không có lỗi, tạo category mới
            var newCategory = new Category
            {
                Name = categoryCreateDTO.Name,
                Description = categoryCreateDTO.Description
            };

            var created = await _categoryRepository.CreateCategoryAsync(newCategory);
            return new CategoryDTO
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description
            };
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with id {id} not found.");
            }
            await _categoryRepository.DeleteCategoryAsync(category);
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return category == null ? null : new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDTO?> UpdateAsync(int id, UpdateCategoryDTO categoryUpdateDTO)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return null;
            }

            // Check trùng tên khi cập nhật
            if (category.Name != categoryUpdateDTO.Name && await _categoryRepository.IsCategoryNameExistsAsync(categoryUpdateDTO.Name))
            {
                throw new InvalidOperationException($"A category with the name '{categoryUpdateDTO.Name}' already exists.");
            }

            category.Name = categoryUpdateDTO.Name;
            category.Description = categoryUpdateDTO.Description;

            await _categoryRepository.UpdateCategoryAsync(id, category);

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
