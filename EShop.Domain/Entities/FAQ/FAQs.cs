using EShop.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Entities.FAQ
{
    public class FAQs : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }

    }
}
