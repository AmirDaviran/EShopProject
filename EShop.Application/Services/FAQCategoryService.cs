using EShop.Application.Interfaces;
using EShop.Application.Utilities.Extensions.Upload;
using EShop.Domain.Entities.FAQ;
using EShop.Domain.Enums.FAQEnum;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.FAQCategory;

namespace EShop.Application.Services;

public class FAQCategoryService(IFAQCategoryRepository _faqCategoryRepository) : IFAQCategoryService
{
    
    public async Task<CreateFAQCategoryResult> CreateFAQCategoryAsync(FAQCategoryCreateViewModel createCategory)
    {

        var category = new FAQCategory
        {
            Name = createCategory.Name,
            DisplayOrder = createCategory.DisplayOrder,
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        };

        if (createCategory.IconFile != null && createCategory.IconFile.Length > 0)
        {
            // استفاده از متد توسعه‌ای برای آپلود فایل
            var fileName = await createCategory.IconFile.SaveAttachmentAsync(SiteTools.FAQCategoryAttachmentsPath);
            if (fileName != null)
            {
                //category.Icon = SiteTools.FAQCategoryAttachmentsPath + fileName;
                category.Icon = fileName; // تغییر اینجا: فقط fileName را استفاده بشه
            }
            else
            {
                // در صورت ناموفق بودن آپلود می‌توان خطایی را مدیریت کرد
                return CreateFAQCategoryResult.Failure;
            }
        }

        await _faqCategoryRepository.AddAsync(category);
        await _faqCategoryRepository.SaveAsync();

        return CreateFAQCategoryResult.Success;
    }

    public async Task<UpdateFAQCategoryResult> UpdateFAQCategoryAsync(FAQCategoryUpdateViewModel updateCategory)
    {
        // دریافت دسته‌بندی موجود بر اساس شناسه
        var category = await _faqCategoryRepository.GetFAQCategoryByIdAsync(updateCategory.Id);
        if (category == null)
        {
            return UpdateFAQCategoryResult.NotFound;
        }

        // در صورتی که فایل جدید آپلود شده باشد
        if (updateCategory.IconFile != null && updateCategory.IconFile.Length > 0)
        {
            // حذف آیکون قبلی
            category.Icon.DeleteImage(SiteTools.FAQCategoryAttachmentsPath);

            // آپلود فایل جدید با استفاده از متد توسعه‌ای
            var fileName = await updateCategory.IconFile.SaveAttachmentAsync(SiteTools.FAQCategoryAttachmentsPath);
            if (fileName == null)
            {
                // در صورت ناموفق بودن آپلود فایل
                return UpdateFAQCategoryResult.Failure;
            }
            category.Icon = fileName;
        }

        // به‌روزرسانی سایر فیلدها
        category.Name = updateCategory.Name;
        category.DisplayOrder = updateCategory.DisplayOrder;

        _faqCategoryRepository.Update(category);
        await _faqCategoryRepository.SaveAsync();

        return UpdateFAQCategoryResult.Success;
    }

    public async Task<List<FAQCategory>> GetFAQCategoriesAsync()
    {
        return await _faqCategoryRepository.GetFAQCategoriesAsync();
    }

    public async Task<FAQCategory> GetFAQCategoryByIdAsync(int id)
    {
        return await _faqCategoryRepository.GetFAQCategoryByIdAsync(id);
    }
 

    public async Task<List<FAQCategory>> GetCategoriesOrderedAsync()
    {
        var categories = await _faqCategoryRepository.GetFAQCategoriesAsync();
        return categories.OrderBy(c => c.DisplayOrder).ToList();
    }


    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _faqCategoryRepository.GetFAQCategoryByIdAsync(id);
        if (category != null)
        {
            category.IsDeleted = true;
            await _faqCategoryRepository.SaveAsync();
        }
    }
}