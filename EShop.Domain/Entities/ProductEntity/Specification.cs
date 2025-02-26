using EShop.Domain.Entities.BaseEntities;
using EShop.Domain.Entities.ProductEntity.Mapping;

namespace EShop.Domain.Entities.ProductEntity;

public class Specification:BaseEntity
{
    public string Name { get; set; }

    #region Relations
    public ICollection<ProductSpecificationMapping> ProductSpecificationMappings { get; set; }
    #endregion
}