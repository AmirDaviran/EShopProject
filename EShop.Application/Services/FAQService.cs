using EShop.Application.Interfaces;
using EShop.Domain.Entities.FAQ;
using EShop.Domain.Enums.FAQEnum;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.FAQ;

namespace EShop.Application.Services
{
    public class FAQService(IFAQRepository _faqRepository) : IFAQService
    {


        #region Admin Side

        #region Get All
        public async Task<List<FAQViewModel>> GetAllAsync()
        {
            var faqs = await _faqRepository.GetAllAsync();
            return faqs.Select(c => new FAQViewModel()
            {
                Id = c.Id,
                Question = c.Question,
                Answer = c.Answer
            }).ToList();
        }
        #endregion

        #region Create

        public async Task<OperationResult> CreateAsync(CreateFAQViewModel model)
        {

            if (model == null || string.IsNullOrWhiteSpace(model.Question) || string.IsNullOrWhiteSpace(model.Answer))
            {
                return OperationResult.Failure;
            }

            FAQs faqs = new FAQs()
            {
                Question = model.Question,
                Answer = model.Answer
            };

            await _faqRepository.InsertAsync(faqs);
            await _faqRepository.SaveAsync();

            return OperationResult.Success;
        }

        #endregion

        #region GetForUpdateAsync

        public async Task<UpdateFAQViewModel> GetForUpdateAsync(int id)
        {
            var faqs = await _faqRepository.GetByIdAsync(id);
            if (faqs == null)
            {
                return null;
            }
            else
            {
                return new UpdateFAQViewModel()
                {
                    Id = faqs.Id,
                    Question = faqs.Question,
                    Answer = faqs.Answer
                };
            }

        }

        #endregion

        #region UpdateAsync

        public async Task<OperationResult> UpdateAsync(UpdateFAQViewModel model)
        {
            if (model == null || model.Id <= 0)
            {
                return OperationResult.Failure;
            }

            var faq = await _faqRepository.GetByIdAsync(model.Id);

            if (faq == null)
            {
                return OperationResult.NotFound;
            }

            faq.Question = model.Question;
            faq.Answer = model.Answer;

            _faqRepository.Update(faq);
            await _faqRepository.SaveAsync();

            return OperationResult.Success;
        }

        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return OperationResult.Failure;
            }

            var faq = await _faqRepository.GetByIdAsync(id);

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

        #region Client Side
        public async Task<List<FAQViewModel>> GetFAQAsync()
        {
            var faq = await _faqRepository.GetAllAsync();

            var faqs = faq
            .Where(p => !p.IsDeleted)
            .OrderBy(p => p.Id)
            .Select(p => new FAQViewModel
            {
                Id = p.Id,
                Question = p.Question,
                Answer = p.Answer

            }).ToList();

            return faqs;
        }


        #endregion

        #endregion

    }


}