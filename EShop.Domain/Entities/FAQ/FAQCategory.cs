using EShop.Domain.Entities.BaseEntities;

namespace EShop.Domain.Entities.FAQ;

public class FAQCategory: BaseEntity
{
    public string Name { get; set; }
    public string Icon { get; set; } // مسیر تصویر آیکون

    #region Relations

    public List<FAQ> FAQs { get; set; }

    #endregion
}