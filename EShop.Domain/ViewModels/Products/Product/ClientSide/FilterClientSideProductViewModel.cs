using EShop.Domain.ViewModels.Common;


namespace EShop.Domain.ViewModels.Products.Product.ClientSide
{
    public class FilterClientSideProductViewModel : BasePaging<ProducClientSidetListViewModel>
    {
        public string SearchTerm { get; set; }
        public string SortBy { get; set; }

    }
}
