using System.ComponentModel.DataAnnotations;

namespace API_Bookstore.Models.DTOs.Book
{
    public class UpdateBookDTO
    {
        [Required(ErrorMessage = "Tiêu đề không được để trống.")]
        [MaxLength(255, ErrorMessage = "Tiêu đề không được vượt quá 255 kí tự.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên tác giả không được để trống.")]
        [MaxLength(255, ErrorMessage = "Tên tác giả không được vượt quá 255 kí tự.")]
        public string Author { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Số lượng tồn kho là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho phải là số nguyên dương.")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "Mã danh mục là bắt buộc.")]
        public int CategoryId { get; set; }
    }
}
