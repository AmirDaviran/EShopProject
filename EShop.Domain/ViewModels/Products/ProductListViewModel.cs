using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.Products
{
    public class ProductListViewModel
    {
        public int productId {  get; set; }

        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        //public string PersianTitle { get; set; }

        public int Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ImageName { get; set; }
        //public ColorListsViewModel? ColorListsView { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }

    }
}
