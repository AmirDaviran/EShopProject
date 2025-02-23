using EShop.Domain.Entities.BaseEntities;
using Microsoft.AspNetCore.Http;

namespace EShop.Domain.Entities.FAQ;

public class FAQCategory: BaseEntity
{
    public string Name { get; set; }
    public string Icon { get; set; } // مسیر تصویر آیکون
    public int DisplayOrder { get; set; }


    #region Relations

    public List<FAQ> FAQs { get; set; }

    #endregion
}