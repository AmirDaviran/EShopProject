using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Enums.ProductEnums
{

    #region Product Enums
    public enum CreateProductResult
    {
        Success,
        NotImage
    }

    public enum EditProductResult
    {
        Success,
        NotFound,
        NotProductSelectedCategory
    }
    #endregion

}
