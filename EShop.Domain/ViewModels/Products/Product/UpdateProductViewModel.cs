using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Products.Product
{
    public class UpdateProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان الزامی است")]
        [StringLength(150, ErrorMessage = "حداکثر 150 کاراکتر مجاز است")]
        public string Title { get; set; }

        [Required(ErrorMessage = "توضیح کوتاه الزامی است")]
        [StringLength(300, ErrorMessage = "حداکثر 300 کاراکتر مجاز است")]
        public string TitleDescription { get; set; }

        [Required(ErrorMessage = "قیمت الزامی است")]
        [Range(1, int.MaxValue, ErrorMessage = "قیمت باید بیشتر از 0 باشد")]
        public int Price { get; set; }

        [StringLength(2000, ErrorMessage = "حداکثر 2000 کاراکتر مجاز است")]
        public string? Review { get; set; }

        [StringLength(2000, ErrorMessage = "حداکثر 2000 کاراکتر مجاز است")]
        public string? ExpertReview { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
