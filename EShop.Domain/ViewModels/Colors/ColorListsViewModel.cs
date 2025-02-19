

namespace EShop.Domain.ViewModels.Colors
{
    public class ColorListsViewModel
    {
        public int ColorId { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public ColorListsViewModel? ColorListsView { get; set; }
    }
}
