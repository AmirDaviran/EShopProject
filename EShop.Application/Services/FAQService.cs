using EShop.Application.Interfaces;
using EShop.Domain.Entities.FAQ;
using EShop.Domain.Enums.FAQEnum;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.FAQ;

namespace EShop.Application.Services;

public class FAQService(IFAQRepository _faqRepository) : IFAQService
{
    #region AdminSide

    #region Update
    public async Task<OperationResult> UpdateAsync(FAQUpdateViewModel model)
    {
        if (model == null || model.Id <= 0)
        {
            return OperationResult.Failure;
        }
        var faq = await _faqRepository.GetFAQByIdAsync(model.Id);

        if (faq == null)
        {
            return OperationResult.NotFound;
        }

        faq.Question = model.Question;
        faq.Answer=model.Answer;
        faq.CategoryId = model.CategoryId;

        _faqRepository.Update(faq);
        await _faqRepository.SaveAsync();

        return OperationResult.Success;
    }
    #endregion

    #region GetForUpdate
    public async Task<FAQUpdateViewModel> GetForUpdateAsync(int id)
    {
        var faqs = await _faqRepository.GetFAQByIdAsync(id);
        if (faqs == null)
        {
            return null;
        }
        else
        {
            return new FAQUpdateViewModel()
            {
                CategoryId = faqs.CategoryId,
                Id = faqs.Id,
                Question = faqs.Question,
                Answer = faqs.Answer
            };
        }

    }
    #endregion

    #region GetAllFAQ
    public async Task<List<FAQViewModel>> GetAllFAQForAdminAsync()
    {
        var faq = await _faqRepository.GetAllFAQAsync();
        var faqs = faq
            .OrderBy(f => f.CreatedDate)
            .Select(f => new FAQViewModel()
            {
                CategoryId = f.CategoryId,
                CategoryName = f.Category?.Name ?? "بدون دسته",
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer,
                CreatedDate = DateTime.Now
            }).ToList();

        return faqs;
    }
    #endregion

    #region Delete
    public async Task<OperationResult> DeleteAsync(int id)
    {
        if (id <= 0)
        {
            return OperationResult.Failure;
        }

        var faq= await _faqRepository.GetFAQByIdAsync(id);

        if (faq == null)
        {
            return OperationResult.NotFound;
        }

        faq.IsDeleted = true;
        _faqRepository.Update(faq);
        await _faqRepository.SaveAsync();

        return OperationResult.Success;
    }
    #endregion

    #region Create
    public async Task<OperationResult> CreateAsync(FAQCreateViewModel model)
    {
        if (model == null || string.IsNullOrWhiteSpace(model.Question) || string.IsNullOrWhiteSpace(model.Answer))
        {
            return OperationResult.Failure;
        }

        FAQ faQs = new FAQ()
        {
            CategoryId = model.CategoryId,
            Question = model.Question,
            Answer = model.Answer,
            CreatedDate = model.CreatedDate,
           
        };

        await _faqRepository.InsertAsync(faQs);
        await _faqRepository.SaveAsync();

        return OperationResult.Success;
    }
    #endregion

    #region GetExplanation

    public async Task<ExplanationDetailViewModel> GetExplanationAsync(int id)
    {
        var faq = await _faqRepository.GetFAQByIdAsync(id);
        if (faq == null) return null;

        return new ExplanationDetailViewModel
        {

            Id = faq.Id,
            Explanation = faq.Explanation
        };
    }

    #endregion

    #endregion

    #region ClientSide

    public async Task<List<FAQViewModel>> GetAllFAQsAsync()
    {
        var faq=await _faqRepository.GetAllFAQAsync();

        var faqs = faq.OrderBy(f => f.Id)
            .Select(f => new FAQViewModel()
            {
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer
                }).ToList();

        return faqs;
    }

  
    public async Task<List<FAQ>> GetFAQsAsync()
    {
        return await _faqRepository.GetAllFAQAsync();
    }

    public async Task<FAQ> GetFAQByIdAsync(int id)
    {
        return await _faqRepository.GetFAQByIdAsync(id);
    }

    public async Task<List<FAQ>> GetFAQsByCategoryIdAsync(int categoryId)
    {
        return await _faqRepository.GetFAQsByCategoryIdAsync(categoryId);
    }
    #endregion

}