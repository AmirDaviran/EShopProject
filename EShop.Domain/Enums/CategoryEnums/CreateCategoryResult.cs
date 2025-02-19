using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Enums.CategoryEnums
{
  public enum CreateCategoryResult
    {
        Success,
        Error,
        DuplicateTitle,
        CannotBeOwnParent,
        InvalidParent
    }
}
