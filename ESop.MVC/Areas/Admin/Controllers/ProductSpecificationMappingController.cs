using EShop.Application.Interfaces;
using EShop.Domain.ViewModels.Products.Product_Specification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        #region List

        [HttpGet]
        public async Task<IActionResult> List(int productId)
        {
            var specifications = await _mappingService.GetSpecificationsByProductIdAsync(productId);
            ViewBag.ProductId = productId;
            return View(specifications);
        }

        #endregion

        #region Add

        [HttpGet]
        public async Task<IActionResult> Add(int productId)
        {
            // گرفتن لیست مشخصات برای دراپ‌داون
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
                // در صورت خطا، لیست مشخصات رو دوباره به ویو بفرست
                var specifications = await _specificationService.GetAllAsync();
                ViewBag.Specifications = new SelectList(specifications, "Id", "Name");
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
            if (result)
            {
                TempData[SuccessMessage] = "مشخصه با موفقیت ویرایش شد";
                return RedirectToAction(nameof(List), new { productId = model.ProductId });
            }

            TempData[ErrorMessage] = "خطا در ویرایش مشخصه";
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
            if (result)
                TempData[SuccessMessage] = "مشخصه با موفقیت حذف شد";
            else
                TempData[ErrorMessage] = "خطا در حذف مشخصه";

            return RedirectToAction(nameof(List), new { productId });
        }
    }

    #endregion
    }