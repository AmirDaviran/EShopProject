using EShop.Application.Interfaces;
using EShop.Domain.Entities.FAQ;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.FAQ;
using EShop.Domain.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services
{
    public class FAQService : IFAQService
    {

        #region Private Field To access Repo
        private readonly IFAQRepository _faqRepository;

        #endregion

        #region Generated Ctor
        public FAQService(IFAQRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }

        #endregion

        #region Methods

        #region Admin Side
        //List
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

        //create

        public async Task<bool> CreateAsync(CreateFAQViewModel model)
        {
           
            FAQs faqs = new FAQs()
            {
                Question = model.Question,
                Answer = model.Answer

            };
            await _faqRepository.InsertAsync(faqs);
            await _faqRepository.SaveAsync();

            return true;

        }


        //Update
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

        public async Task<bool> UpdateAsync(UpdateFAQViewModel model)
        {
            var faq = await _faqRepository.GetByIdAsync(model.Id);

            if (faq == null)
            {
                return false;
            }
            else
            {
                faq.Question = model.Question;
            }
            _faqRepository.Update(faq);
            await _faqRepository.SaveAsync();

            return true;

        }

        //Delete
        public async Task<bool> DeleteAsync(int id)
        {
            FAQs faq = await _faqRepository.GetByIdAsync(id);
            if (faq == null)

                return false;



            faq.IsDeleted = true;
            _faqRepository.Update(faq);
            await _faqRepository.SaveAsync();

            return true;
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
