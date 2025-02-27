using EShop.Application.Interfaces;
using EShop.Domain.ViewModels.Products.Product_Specification;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class ProductSpecificationMappingController : AdminBaseController
    {
        private readonly IProductSpecificationMappingService _mappingService;
        private readonly IProductService _productService;
        private readonly ISpecificationService _specificationService;

        public ProductSpecificationMappingController(
            IProductSpecificationMappingService mappingService,
            IProductService productService,
            ISpecificationService specificationService)
        {
            _mappingService = mappingService;
            _productService = productService;
            _specificationService = specificationService;
        }

        [HttpGet]
        public async Task<IActionResult> List(int productId)
        {
            var specifications = await _mappingService.GetSpecificationsByProductIdAsync(productId);
            ViewBag.ProductId = productId;
            return View(specifications);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int productId)
        {
            var model = new AddSpecificationToProductViewModel { ProductId = productId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSpecificationToProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(model);
            }

            var result = await _mappingService.AddSpecificationToProductAsync(model);
            if (result)
            {
                TempData[SuccessMessage] = "مشخصه با موفقیت به محصول اضافه شد";
                return RedirectToAction(nameof(List), new { productId = model.ProductId });
            }

            TempData[ErrorMessage] = "خطا در اضافه کردن مشخصه";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int mappingId, int productId)
        {
            var result = await _mappingService.RemoveSpecificationFromProductAsync(mappingId);
            if (result)
                TempData[SuccessMessage] = "مشخصه با موفقیت حذف شد";
            else
                TempData[ErrorMessage] = "خطا در حذف مشخصه";

            return RedirectToAction(nameof(List), new { productId });
        }
    }
}