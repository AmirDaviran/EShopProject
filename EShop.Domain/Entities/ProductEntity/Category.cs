using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.Domain.Entities.BaseEntities;

namespace EShop.Domain.Entities.ProductEntity;

public class Category : BaseEntity
{
    [MaxLength(100)]
    public string Title { get; set; }

    public int? ParentCategoryId { get; set; }

    [ForeignKey(nameof(ParentCategoryId))]
    public virtual Category? ParentCategory { get; set; }

    public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();

    public int DisplayOrder { get; set; }

    public ICollection<CategorySpecificationMapping> CategorySpecificationMappings { get; set; }

}