using EShop.Domain.Entities.Colors;

namespace EShop.Domain.ViewModels.Colors.Product_Color
{
    public class AddProductColorViewModel
    {
        public int ProductId { get; set; }
        List<Color> Colors { get; set; }
        public int Price { get; set; }
        public int DisparityAmount { get; set; }
        public int SelectedColorId { get; set; }
       
    }
}
