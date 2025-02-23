using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Products.Product
{
    public class CreateProductViewModel
    {
        public string Title { get; set; }
        public string TitleDescription { get; set; }
        public int Price { get; set; }
        public string? Review { get; set; }
        public string? ExpertReview { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? ImageFile { get; set; }
        
    }
}
