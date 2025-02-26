using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.Domain.Entities.BaseEntities;

namespace EShop.Domain.Entities.ProductEntity;

public class Category : BaseEntity
{
    [MaxLength(100)]
    public string Title { get; set; }

    public int? ParentCategoryId { get; set; }

    public int DisplayOrder { get; set; }


    #region Relation
    [ForeignKey(nameof(ParentCategoryId))]
    public  Category? ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();
   // public ICollection<Product> Products { get; set; } = new List<Product>(); 
    #endregion

}