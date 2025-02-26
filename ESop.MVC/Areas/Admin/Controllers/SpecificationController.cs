using EShop.Application.Interfaces;
using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.Enums.SpecificationEnum;
using EShop.Domain.ViewModels.Products.Specification;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class SpecificationController(ISpecificationService _specificationService) : AdminBaseController
    {

        #region List
        public async Task<IActionResult> List(FilterSpecificationViewModel model)
        {
            var result = await _specificationService.FilterAsync(model);
            return View(result);
        }
        #endregion

        #region Create

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSpecificationViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            var result = await _specificationService.CreateAsync(model);
            switch (result)
            {
                case CreateSpecificationResult.Success:
                    TempData[SuccessMessage] = "مشخصه با موفقیت ایجاد شد";
                    return RedirectToAction(nameof(List));
                case CreateSpecificationResult.InvalidInput:
                    TempData[ErrorMessage] = "ورودی نامعتبر است";
                    break;
                case CreateSpecificationResult.NotFound:
                    TempData[ErrorMessage] = "مشخصه یافت نشد";
                    break;
            }
            return View(model);
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _specificationService.GetForUpdateAsync(id);
            if (model == null)
            {
                TempData[ErrorMessage] = "محصول مورد نظر یافت نشد";
                return RedirectToAction(nameof(List));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSpecificationViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(model);
            }
            #endregion

            var result = await _specificationService.UpdateAsync(model);
            switch (result)
            {
                case UpdateSpecificationResult.Success:
                    TempData[SuccessMessage] = "مشخصه با موفقیت ویرایش شد";
                    return RedirectToAction(nameof(List));
                case UpdateSpecificationResult.NotFound:
                    TempData[ErrorMessage] = "مشخصه مورد نظر یافت نشد";
                    break;
                case UpdateSpecificationResult.InvalidInput:
                    TempData[ErrorMessage] = "ورودی نامعتبر است";
                    break;
            }
            return View(model);
        }

        #endregion

        #region Delete

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _specificationService.DeleteAsync(id);

            if (result == DeleteSpecificationResult.Success)
            {
                TempData[SuccessMessage] = "محصول با موفقیت حذف شد";
            }

            else if (result == DeleteSpecificationResult.NotFound)
            {
                TempData[ErrorMessage] = "محصول مورد نظر یافت نشد";
            }

            return RedirectToAction(nameof(List));
        }

        #endregion

    }
}
