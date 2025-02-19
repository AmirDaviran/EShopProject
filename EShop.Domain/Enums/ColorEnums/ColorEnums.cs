using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Enums.ColorEnums
{
    public enum CreateColorResult
    {
        Success,
        Failed
    }

    public enum EditColorResult
    {
        Success, 
        NotFound
    }



    #region ProductColorMapping Enums
    public enum CreateProductColorMappingResult
    {
        Success,
        NotFound,
        Failed
    }

    public enum EditProductColorMappingResult
    {
        Success,
        NotFound
    }

    public enum AddProductColorResult
    {
        Success,
        NotFound,
        Failed
    }
    #endregion
}
