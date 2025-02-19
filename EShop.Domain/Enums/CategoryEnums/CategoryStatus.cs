using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Enums.CategoryEnums
{
    public enum CategoryStatus
    {
        [Display(Name = "فعال")]
        Active,
        [Display(Name = "غیرفعال")]
        Inactive
    }
}
