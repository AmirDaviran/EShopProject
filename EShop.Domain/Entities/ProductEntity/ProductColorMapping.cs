using EShop.Domain.Entities.BaseEntities;
using EShop.Domain.Entities.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Entities.ProductEntity
{
    public class ProductColorMapping : BaseEntity
    {
        #region Properties 
        public int ProductId { get; set; }
        public int ColorId { set; get; }
        public int Amount { get; set; }
        #endregion

        #region Relations
        public Color Color { get; set; }
        #endregion
    }
}
