using EShop.Domain.Entities.BaseEntities;

namespace EShop.Domain.Entities.FAQ;

public class FAQs: BaseEntity
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public string? Explanation { get; set; }

}