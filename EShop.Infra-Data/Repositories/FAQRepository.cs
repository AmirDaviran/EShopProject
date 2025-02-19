using EShop.Domain.Entities.FAQ;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.FAQ;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infra_Data.Repositories
{
    public class FAQRepository : IFAQRepository
    {
        #region Private Field to Access Ctor for Context

        private readonly EShopDbContext _context;

        #endregion

        #region Ctor

        public FAQRepository(EShopDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods Implementation
        //Showing List
        public async Task<List<FAQs>> GetAllAsync()
        {
            return await _context.FAQs
                .Where(c=>!c.IsDeleted)
                .ToListAsync();
        }


        //create
        public async Task InsertAsync(FAQs faqs)
        {
           await _context.FAQs.AddAsync(faqs);
        }    

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        //Update

        public async Task<FAQs> GetByIdAsync(int id)
        {
             return await _context.FAQs
                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(FAQs faqs)
        {
            _context.FAQs.Update(faqs);
        }
        #endregion

    }
}
