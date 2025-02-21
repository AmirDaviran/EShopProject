using EShop.Domain.Entities.BaseEntities;
namespace EShop.Domain.Entities.FAQ;
public class FAQ: BaseEntity
{

    public string Question { get; set; }
    public string Answer { get; set; }
    public string? Explanation { get; set; } 

    #region Relations
    public int CategoryId { get; set; }
    public FAQCategory Category { get; set; }

    #endregion

}