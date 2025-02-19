using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Colors.Product_Color
{
    public class ProductWithColorsViewModel
    {
        public int ProductId { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ColorMappingViewModel> ColorMappings { get; set; }
    }
}
