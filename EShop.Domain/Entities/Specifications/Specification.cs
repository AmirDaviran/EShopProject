using EShop.Domain.Entities.BaseEntities;
using EShop.Domain.Entities.ProductEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Entities.Specifications
{
    public class Specification : BaseEntity
    {
        #region Properties
        public string Name { get; set; }
        #endregion

        #region Relations
        public ICollection<CategorySpecificationMapping> CategorySpecificationMappings { get; set; }
        public ICollection<ProductSpecificationMapping> ProductSpecificationMappings { get; set; }
        #endregion
    }
}
