using API_Bookstore.Models.DTOs.Category;

namespace API_Bookstore.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<CategoryDTO> CreateAsync(CreateCategoryDTO categoryCreateDTO);
        Task<CategoryDTO?> UpdateAsync(int id, UpdateCategoryDTO categoryUpdateDTO);
        Task DeleteAsync(int id);
    }
}
