using EShop.Application.Interfaces;
using EShop.Domain.Enums.ProductSpecificationMapping;
using EShop.Domain.ViewModels.Products.Product_Specification;
using EShop.Domain.ViewModels.Products.Specification;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        #region Actions

        #region Create

        [HttpGet]
        public async Task<IActionResult> List(int productId)
        {
            var mappings = await _mappingService.GetByProductIdAsync(productId);
            ViewBag.ProductId = productId;
            return View(mappings);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create(int productId)
        {
            ViewBag.ProductId = productId;
            var specifications = await _specificationService.FilterAsync(new FilterSpecificationViewModel());
            ViewBag.Specifications = specifications.Entities;
            return View(new SpecificationProductViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(int productId, SpecificationProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(model);
            }

            var result = await _mappingService.CreateAsync(productId, model);
            switch (result)
            {
                case CreateProductSpecificationResult.Success:
                    TempData[SuccessMessage] = "مشخصه محصول با موفقیت اضافه شد";
                    return RedirectToAction(nameof(List), new { productId });
                case CreateProductSpecificationResult.InvalidInput:
                    TempData[ErrorMessage] = "ورودی نامعتبر است";
                    break;
                case CreateProductSpecificationResult.NotFound:
                    TempData[ErrorMessage] = "محصول یا مشخصه یافت نشد";
                    break;
            }
            return View(model);
        }

        #endregion

        #endregion




        [HttpGet]
        public async Task<IActionResult> Update(int mappingId)
        {
            var mapping = await _mappingService.GetByProductIdAsync(mappingId); 
            var spec = mapping.FirstOrDefault(m => m.SpecificationId == mappingId);
            if (spec == null)
            {
                TempData[ErrorMessage] = "مشخصه محصول یافت نشد";
                return RedirectToAction(nameof(List));
            }
            var specifications = await _specificationService.FilterAsync(new FilterSpecificationViewModel());
            ViewBag.Specifications = specifications.Entities;
            return View(new SpecificationProductViewModel { SpecificationId = spec.SpecificationId, Value = spec.Value });
        }

        [HttpPost]
        public async Task<IActionResult> Update(int mappingId, SpecificationProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(model);
            }

            var result = await _mappingService.UpdateAsync(mappingId, model);
            switch (result)
            {
                case UpdateProductSpecificationResult.Success:
                    TempData[SuccessMessage] = "مشخصه محصول با موفقیت ویرایش شد";
                    return RedirectToAction(nameof(List));
                case UpdateProductSpecificationResult.InvalidInput:
                    TempData[ErrorMessage] = "ورودی نامعتبر است";
                    break;
                case UpdateProductSpecificationResult.NotFound:
                    TempData[ErrorMessage] = "مشخصه محصول یافت نشد";
                    break;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int mappingId)
        {
            var result = await _mappingService.DeleteAsync(mappingId);
            if (result == DeleteProductSpecificationResult.Success)
            {
                TempData[SuccessMessage] = "مشخصه محصول با موفقیت حذف شد";
            }
            else
            {
                TempData[ErrorMessage] = "مشخصه محصول یافت نشد";
            }
            return RedirectToAction(nameof(List));
        }
    }
}