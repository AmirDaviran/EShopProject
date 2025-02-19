using EShop.Domain.Entities.FAQ;
using EShop.Domain.ViewModels.FAQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface IFAQRepository
    {
        //list
        Task<List<FAQs>> GetAllAsync();

        //create

        Task InsertAsync(FAQs faqs);

        Task SaveAsync();

        //Update
        Task<FAQs> GetByIdAsync(int id);

        void Update(FAQs faqs); 

    }
}
