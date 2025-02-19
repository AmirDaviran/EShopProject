using EShop.Application.Interfaces;
using EShop.Domain.ViewModels.FAQ;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class FAQController(IFAQService fAQService) :AdminBaseController
    {


        #region List
        [HttpGet]
        public async Task<IActionResult> List() 
        {
            var  model =  await fAQService.GetAllAsync();
            return View(model);
        }

        #endregion


        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFAQViewModel model) 
        {
            #region Validation
            if (!ModelState.IsValid) 
            {   
                return View(model);
            }
            #endregion

            var result = await fAQService.CreateAsync(model);
            if (result) 
            {
                return RedirectToAction(nameof(List));
            }
            return View();
        }
        #endregion



        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id) 
        {
            var faqs = await fAQService.GetForUpdateAsync(id);
            if (faqs == null) 
            {
                return NotFound();
            }
            return View(faqs);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateFAQViewModel model) 
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            var result = await fAQService.UpdateAsync(model);
            if (result)
            {
                return RedirectToAction(nameof(List));
            }
            else 
            {
                return View(model);
            }
        
        }
        #endregion


        #region Delete
        public async Task<IActionResult> Delete(int id) 
        {
            var result = await fAQService.DeleteAsync(id);
            if (result) 
            {
                TempData["SuccessMessage"] = "حذف با موفقیت انجام شد";
                return RedirectToAction(nameof(List));


            }
            TempData["WarningMessage"] = "محصول مورد نظر یافت نشد.";
            return RedirectToAction(nameof(List));
        }

        #endregion



    }
}
