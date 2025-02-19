using EShop.Domain.Entities.BaseEntities;


namespace EShop.Domain.Entities.ProductEntity
{
    public class ProductSelectedCategory : BaseEntity
    {
        #region Properties
        public int ProductId {  get; set; }

        public int CategoryId { get; set; } = 0;

        #endregion

        #region Relations
        public Product Product { get; set; }
        public Category Category { get; set; }
        #endregion
    }
}
