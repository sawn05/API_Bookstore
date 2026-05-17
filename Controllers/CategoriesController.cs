using API_Bookstore.Models.DTOs.Category;
using API_Bookstore.Repositories;
using API_Bookstore.Repositories.Interfaces;
using API_Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var data = await _categoryService.GetAllAsync();
            return Ok(new { 
                success = true, 
                message = "Get data successfully", 
                data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound(new { 
                    success = false, 
                    message = $"Category with id {id} not found" 
                });
            }
            return Ok(new
            {
                success = true,
                message = $"Get data by id {id} successfully",
                category
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid input" });

            try
            {
                var createdCategory = await _categoryService.CreateAsync(dto);
                return Ok(new
                {
                    success = true,
                    message = "Category created successfully",
                    category = createdCategory
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while creating the category", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDTO dto)
        {
            if (id <= 0)
                return BadRequest(new { success = false, message = "Invalid id" });

            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid input" });

            try
            {
                var updatedCategory = await _categoryService.UpdateAsync(id, dto);

                if (updatedCategory == null)
                {
                    return NotFound(new { success = false, message = $"Category with id {id} not found" });
                }

                return Ok(new
                {
                    success = true,
                    message = "Category updated successfully",
                    category = updatedCategory
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while updating the category", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { success = false, message = "Invalid id" });
            try
            {
                await _categoryService.DeleteAsync(id);
                return Ok(new { success = true, message = "Category deleted successfully" });
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the category", error = ex.Message });
            }
        }
    }
}
