using EShop.Domain.ViewModels.Products.Product_Specification;

namespace EShop.Domain.ViewModels.Products.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string TitleDescription { get; set; }
        public int Price { get; set; }
        public string? Review { get; set; }
        public string? ExpertReview { get; set; }
        public string? ImageName { get; set; }

    }

}

