

using EShop.Domain.Entities.Colors;

namespace EShop.Domain.ViewModels.Colors.Product_Color
{
    public class CreateProductColorMappingViewModel
    {
        public int ProductId { get; set; }
        public int SelectedColorId { get; set; }
        public int Amount { get; set; } = 0;
        //public List<Color> Colors { get; set; }
    }
}
