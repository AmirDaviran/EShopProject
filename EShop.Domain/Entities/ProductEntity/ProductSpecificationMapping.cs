using EShop.Domain.Entities.BaseEntities;
using EShop.Domain.Entities.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Entities.ProductEntity
{
    public class ProductSpecificationMapping : BaseEntity
    {
        #region Properties
        public int ProductId { get; set; }
        public int SpecificationId { get; set; }
        public string Value { get; set; }
        #endregion

        #region Relations
        public Product Product { get; set; }
        public Specification Specification { get; set; }
        #endregion


    }
}
