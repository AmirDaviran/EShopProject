using EShop.Domain.Entities.BaseEntities;

namespace EShop.Domain.Entities.ProductEntity.Mapping;

public class ProductSpecificationMapping: BaseEntity
{
    #region Properties

    public string Value { get; set; }
    public int ProductId { get; set; }
    public int SpecificationId { get; set; }

    #endregion

    #region Relation
    public Product Product { get; set; }
    public Specification Specification { get; set; }
    #endregion
}