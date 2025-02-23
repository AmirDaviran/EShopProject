using EShop.Application.Interfaces;
using EShop.Domain.Enums.ColorEnums;
using EShop.Domain.ViewModels.Colors;
using EShop.Domain.ViewModels.Colors.Product_Color;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class ColorController(IColorService _colorService) : AdminBaseController
    {

        #region List Colors 
        public async Task<IActionResult> Index()
        {
            var colors = await _colorService.GetAllColorsList();
            return View(colors);
        }
        #endregion

        #region Create Color Action
        [HttpGet]
        public async Task<IActionResult> CreateColor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateColor(CreateColorViewModel model)
        {
            #region validation

            if (!ModelState.IsValid)
                return View(model);  
            #endregion


            var result = await _colorService.CreateColor(model);

            if (result == CreateColorResult.Success)
            {
                TempData["SuccessMessage"] = "ثبت رنگ با موفقیت انجام شد.";
                return RedirectToAction("Index");
            }

            else TempData["WarningMessage"] = "ثبت رنگ انجام نشد!";


            return View(model);

        }
        #endregion

        #region Edit Color Action
        [HttpGet]
        public async Task<IActionResult> EditColor(int colorId)
        {
            var color = await _colorService.GetColorByColorId(colorId);
            EditColorViewModel model = new EditColorViewModel()
            {
                ColorId = colorId,
                Name = color.Name,
                Code = color.Code,
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditColor(EditColorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _colorService.EditColor(model);

                switch (result)
                {
                    case EditColorResult.NotFound:
                        TempData[WarningMessage] = "رنگ مورد نظر یافت نشد!";
                        break;
                        case EditColorResult.Success:
                        TempData[SuccessMessage] = "رنگ با موفقیت ویرایش شد.";

                        return RedirectToAction("Index");
                }

            }
            return View(model);
        }
        #endregion

        #region Delete Color Action

        public async Task<IActionResult> DeleteColor(int colorId)
        {
            var result = await _colorService.DeleteColor(colorId);
            if(result)
            {
                TempData[SuccessMessage] = "حذف با موفقیت انجام شد.";
                return RedirectToAction("Index");
            }
            TempData[ErrorMessage] = "حذف انجام نشد!";
            return RedirectToAction("Index");
        }

        #endregion
   
    }
}
