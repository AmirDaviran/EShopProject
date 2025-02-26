using EShop.Domain.Entities.BaseEntities;


namespace EShop.Domain.Entities.ProductEntity.Mapping
{
    public class CategorySpecificationMapping : BaseEntity
    {
        #region Properties
        public int CategoryId { get; set; }
        public int SpecificationId { get; set; }
        #endregion

        #region Relations
        public Category Category { get; set; }
        public Specification Specification { get; set; }
        #endregion
    }
}
