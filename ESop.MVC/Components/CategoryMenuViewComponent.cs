using EShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Components
{
    public class CategoryMenuViewComponent(ICategoryService _categoryService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetMainCategoriesAsync();
            return View("CategoryMenu", categories);
        }

    }
}
