using EShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        #region Fields

        private readonly ICategoryService _categoryService;

        #endregion

        #region Constructor

        public CategoryMenuViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetMainCategoriesAsync();
            return View("CategoryMenu", categories);
        }

    }
}
