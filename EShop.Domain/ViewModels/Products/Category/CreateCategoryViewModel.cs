using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.Products.Category
{
    public class CreateCategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا عنوان را وارد کنید")]
        [MaxLength(100, ErrorMessage = "عنوان نمی‌تواند بیشتر از 100 کاراکتر باشد.")]
        public string Title { get; set; }

        public int? ParentCategoryId { get; set; }

        [Required(ErrorMessage = "لطفا ترتیب نمایش را وارد کنید")]
        public int DisplayOrder { get; set; }
    }

}
