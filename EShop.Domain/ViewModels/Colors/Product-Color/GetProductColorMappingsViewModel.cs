using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Colors.Product_Color
{
    public class GetProductColorMappingsViewModel
    {
        public int ProductId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

    }
}
