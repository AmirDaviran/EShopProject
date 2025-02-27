using EShop.Application.Interfaces;
using EShop.Domain.Enums.ProductSpecificationMapping;
using EShop.Domain.ViewModels.Products.Product_Specification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class ProductSpecificationMappingController : AdminBaseController
    {
        #region Fields

        private readonly IProductSpecificationMappingService _mappingService;
        private readonly IProductService _productService;
        private readonly ISpecificationService _specificationService;

        #endregion

        #region Constructor

        public ProductSpecificationMappingController(
            IProductSpecificationMappingService mappingService,
            IProductService productService,
            ISpecificationService specificationService)
        {
            _mappingService = mappingService;
            _productService = productService;
            _specificationService = specificationService;
        }

        #endregion

        #region List

        [HttpGet]
        public async Task<IActionResult> List(int productId, ProductSpecificationFilterViewModel model)
        {
            if (productId <= 0 && model.ProductId <= 0)
            {
                return RedirectToAction("List", "Product"); // یا یک صفحه خطا
            }

            model.ProductId = productId > 0 ? productId : model.ProductId; // اولویت با productId از URL
            var result = await _mappingService.FilterAsync(model);
            ViewBag.ProductId = model.ProductId; // ارسال به ویو
            return View(result);
        }

        #endregion

        #region Add

        [HttpGet]
        public async Task<IActionResult> Add(int productId)
        {
            var specifications = await _specificationService.GetAllAsync();
            ViewBag.Specifications = new SelectList(specifications, "Id", "Name");

            var model = new AddSpecificationToProductViewModel { ProductId = productId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSpecificationToProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var specifications = await _specificationService.GetAllAsync();
                ViewBag.Specifications = new SelectList(specifications, "Id", "Name");
                TempData[ErrorMessage] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(model);
            }

            var result = await _mappingService.AddSpecificationToProductAsync(model);
            switch (result)
            {
                case ProductSpecificationMappingResult.Success:
                    TempData[SuccessMessage] = "مشخصه با موفقیت به محصول اضافه شد";
                    return RedirectToAction(nameof(List), new { productId = model.ProductId });
                case ProductSpecificationMappingResult.InvalidInput:
                    TempData[ErrorMessage] = "ورودی نامعتبر است";
                    break;
                case ProductSpecificationMappingResult.Failed:
                    TempData[ErrorMessage] = "خطا در اضافه کردن مشخصه";
                    break;
                default:
                    TempData[WarningMessage] = "وضعیت نامشخص در اضافه کردن مشخصه";
                    break;
            }

            var specsOnError = await _specificationService.GetAllAsync();
            ViewBag.Specifications = new SelectList(specsOnError, "Id", "Name");
            return View(model);
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(int mappingId, int productId)
        {
            var mappings = await _mappingService.GetSpecificationsByProductIdAsync(productId);
            var specToEdit = mappings.FirstOrDefault(m => m.MappingId == mappingId);
            if (specToEdit == null)
            {
                TempData[ErrorMessage] = "مشخصه یافت نشد";
                return RedirectToAction(nameof(List), new { productId });
            }

            var model = new AddSpecificationToProductViewModel
            {
                ProductId = productId,
                SpecificationId = specToEdit.SpecificationId,
                Value = specToEdit.Value
            };

            var specifications = await _specificationService.GetAllAsync();
            ViewBag.Specifications = new SelectList(specifications, "Id", "Name", specToEdit.SpecificationId);
            ViewBag.MappingId = mappingId; // برای ارسال به ویو
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AddSpecificationToProductViewModel model, int mappingId)
        {
            if (!ModelState.IsValid)
            {
                var specifications = await _specificationService.GetAllAsync();
                ViewBag.Specifications = new SelectList(specifications, "Id", "Name", model.SpecificationId);
                ViewBag.MappingId = mappingId;
                TempData[ErrorMessage] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(model);
            }

            var result = await _mappingService.UpdateSpecificationAsync(mappingId, model);
            switch (result)
            {
                case ProductSpecificationMappingResult.Success:
                    TempData[SuccessMessage] = "مشخصه با موفقیت ویرایش شد";
                    return RedirectToAction(nameof(List), new { productId = model.ProductId });
                case ProductSpecificationMappingResult.InvalidInput:
                    TempData[ErrorMessage] = "ورودی نامعتبر است";
                    break;
                case ProductSpecificationMappingResult.NotFound:
                    TempData[ErrorMessage] = "مشخصه مورد نظر یافت نشد";
                    break;
                case ProductSpecificationMappingResult.Failed:
                    TempData[ErrorMessage] = "خطا در ویرایش مشخصه";
                    break;
                default:
                    TempData[WarningMessage] = "وضعیت نامشخص در ویرایش مشخصه";
                    break;
            }

            var specsOnError = await _specificationService.GetAllAsync();
            ViewBag.Specifications = new SelectList(specsOnError, "Id", "Name", model.SpecificationId);
            ViewBag.MappingId = mappingId;
            return View(model);
        }

        #endregion

        #region Delete

        [HttpPost]
        public async Task<IActionResult> Delete(int mappingId, int productId)
        {
            var result = await _mappingService.RemoveSpecificationFromProductAsync(mappingId);
            switch (result)
            {
                case ProductSpecificationMappingResult.Success:
                    TempData[SuccessMessage] = "مشخصه با موفقیت حذف شد";
                    break;
                case ProductSpecificationMappingResult.NotFound:
                    TempData[ErrorMessage] = "مشخصه مورد نظر یافت نشد";
                    break;
                case ProductSpecificationMappingResult.Failed:
                    TempData[ErrorMessage] = "خطا در حذف مشخصه";
                    break;
                default:
                    TempData[WarningMessage] = "وضعیت نامشخص در حذف مشخصه";
                    break;
            }

            return RedirectToAction(nameof(List), new { productId });
        }

        #endregion
    }
}