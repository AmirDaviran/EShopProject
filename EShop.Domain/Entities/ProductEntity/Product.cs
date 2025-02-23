using EShop.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Entities.ProductEntity
{
    public class Product :BaseEntity
    {
        #region Properties

        [MaxLength(500)]
        public string PersianTitle { get; set; }

        [MaxLength(500)]
        public string EnglishTitle { get; set; }
        public string? Review {  get; set; }
        public string? ExpertReview { get; set; }
        public string? ImageName { get; set; }
        public int Price { get; set; }
        #endregion

        #region Relations
        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        #endregion
    }
}


