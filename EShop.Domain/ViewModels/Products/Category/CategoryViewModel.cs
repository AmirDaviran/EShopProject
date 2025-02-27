namespace EShop.Domain.ViewModels.Products.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentCategoryId { get; set; }
        public string ParentCategoryTitle { get; set; }
        public int DisplayOrder { get; set; }
        public List<CategoryViewModel> SubCategories { get; set; } = new();
    }

}
