
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.Products.Product.ClientSide
{
    // جزئیات محصول
    public class ProductDetailsViewModel
    {
        public string Title { get; set; }
        public string TitleDescription { get; set; }
        public int Price { get; set; }
        public string ImageName { get; set; }
    }

    // مشخصات فنی
    public class SpecificationViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ProductSpecificationsViewModel
    {
        public List<SpecificationViewModel> Specifications { get; set; }
    }

    // دسته‌بندی‌ها
    public class CategoryViewModel
    {
        public string Title { get; set; }
        public int? ParentCategoryId { get; set; }
        public string ParentCategoryTitle { get; set; }
    }

    public class ProductCategoriesViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
    }

    // نقد و بررسی
    public class ProductReviewsViewModel
    {
        public string Review { get; set; }
        public string ExpertReview { get; set; }
    }

    // مدل جامع برای بخش‌های شما
    public class MyProductSectionsViewModel
    {
        public ProductDetailsViewModel ProductDetails { get; set; }
        public ProductSpecificationsViewModel ProductSpecifications { get; set; }
        public ProductCategoriesViewModel ProductCategories { get; set; }
        public ProductReviewsViewModel ProductReviews { get; set; }
    }
}
