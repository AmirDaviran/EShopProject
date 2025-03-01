﻿using EShop.Domain.Entities.BaseEntities;
using EShop.Domain.Entities.ProductEntity.Mapping;

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

        #region Relations
        public ICollection<ProductSpecificationMapping> ProductSpecificationMappings { get; set; }
        public ICollection<ProductCategoryMapping> ProductCategoryMappings { get; set; }

        #endregion
    }
}


