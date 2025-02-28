// EShop.Presentation/ViewModels/ProductClientViewModels.cs
namespace EShop.Domain.ViewModels.Products.ClientSide;


    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string TitleDescription { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Review { get; set; }
        public string ExpertReview { get; set; }
        public List<ProductVariantViewModel> Variants { get; set; } = new List<ProductVariantViewModel>();
        public List<ProductSpecificationViewModel> Specifications { get; set; } = new List<ProductSpecificationViewModel>();
        public List<SellerViewModel> Sellers { get; set; } = new List<SellerViewModel>();
        public List<RelatedProductViewModel> RelatedProducts { get; set; } = new List<RelatedProductViewModel>();
        public double AverageRating { get; set; }
        public int RatingCount { get; set; }
        public string CategoryTitle { get; set; }
    }

    public class ProductVariantViewModel
    {
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public bool IsSelected { get; set; }
    }

    public class ProductSpecificationViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class SellerViewModel
    {
        public string SellerName { get; set; }
        public decimal Price { get; set; }
        public string Warranty { get; set; }
        public double SatisfactionRate { get; set; }
        public int Stock { get; set; }
    }

    public class RelatedProductViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }
        public List<string> VariantColors { get; set; } = new List<string>();
    }
