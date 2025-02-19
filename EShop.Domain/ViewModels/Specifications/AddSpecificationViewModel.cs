using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Specifications
{
    public class AddSpecificationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> SelectedCategories { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
