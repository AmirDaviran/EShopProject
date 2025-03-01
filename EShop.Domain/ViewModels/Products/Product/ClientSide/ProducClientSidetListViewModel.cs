using EShop.Domain.Entities.ProductEntity.Mapping;

namespace EShop.Domain.ViewModels.Products.Product.ClientSide
{
   public class ProducClientSidetListViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<ProductCategoryMapping>? ProductCategoryMappings { get; set; }
    }
}
