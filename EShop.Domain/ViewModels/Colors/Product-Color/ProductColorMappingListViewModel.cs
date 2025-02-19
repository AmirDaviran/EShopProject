

namespace EShop.Domain.ViewModels.Colors.Product_Color
{
    public class ProductColorMappingListViewModel
    {
        public int MappingId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Code {  get; set; } = string.Empty;  
        public DateTime CreatedDate { get; set; }
    }
}
