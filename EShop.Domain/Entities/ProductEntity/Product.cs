using EShop.Domain.Entities.BaseEntities;

namespace EShop.Domain.Entities.ProductEntity
{
    public class Product : BaseEntity
    {
        #region Properties

        public string Title { get; set; }
        public string TitleDescription { get; set; }
        public int Price { get; set; }
        public string? Review { get; set; }
        public string? ExpertReview { get; set; }
        public string? ImageName { get; set; }

        #endregion
    }
}


