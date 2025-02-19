using EShop.Domain.Entities.BaseEntities;
using EShop.Domain.Entities.ProductEntity;

namespace EShop.Domain.Entities.Colors
{
    public class Color : BaseEntity
    {
        #region Properties
        public string Name { get; set; }

        public string Code { get; set; }
        #endregion

        #region Relations
        public ICollection<ProductColorMapping> ProductColorMappings {  get; set; } 
        #endregion
    }
}
