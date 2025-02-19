using EShop.Application.Interfaces;
using EShop.Domain.Enums.SpesificationEnums;
using EShop.Domain.ViewModels.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class SpecificationController : AdminBaseController
    {
        #region Constructor
        private readonly ISpecificationService _specificationService;
        private readonly ICategoryService _categoryService;
        public SpecificationController(ISpecificationService specificationService,
                                       ICategoryService categoryService)
        {
            _specificationService = specificationService;
            _categoryService = categoryService;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }

        #region Add Specification To Categories

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = await _categoryService.GetAllCategoriesInForm();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddSpecificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _specificationService.AddAsync(model);
                switch (result)
                {
                    case AddSpecificationResult.Failed:
                        TempData[WarningMessage] = "فیلد نام مشخصه خالی است!";
                        break;
                    case AddSpecificationResult.Success:
                        TempData[SuccessMessage] = "نام مشخصه با موفقیت ثبت شد.";
                        ViewData["Categories"] = await _categoryService.GetAllCategoriesInForm();
                        return View(model);
                }
            }
            ViewData["Categories"] = await _categoryService.GetAllCategoriesInForm();
            return View();
        }
        #endregion
    }
}
