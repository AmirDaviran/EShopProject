using EShop.Domain.Entities.BaseEntities;
using EShop.Domain.Entities.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Entities.ProductEntity
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
