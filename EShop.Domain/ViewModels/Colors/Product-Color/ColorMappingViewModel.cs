using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Colors.Product_Color
{
    public class ColorMappingViewModel
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
      public int Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
