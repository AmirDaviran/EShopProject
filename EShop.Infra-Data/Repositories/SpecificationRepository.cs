using EShop.Domain.Entities.Specifications;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infra_Data.Repositories
{
    public class SpecificationRepository : ISpecificationRepository
    {
        #region Context
        private readonly EShopDbContext _context;
        public SpecificationRepository(EShopDbContext context)
        {
            _context = context;
        }
        #endregion


        public async Task<Specification?> GetByIdAsync(int id)
        {
            return await _context.Specifications.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<List<Specification>> GetAllAsync()
        {
            return await _context.Specifications.ToListAsync();
        }

        public async Task AddAsync(Specification specification)
        {
            await _context.Specifications.AddAsync(specification);
        }

        public async Task UpdateAsync(Specification specification)
        {
            _context.Specifications.Update(specification);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var specification = await GetByIdAsync(id);
            if (specification != null)
            {
                specification.IsDeleted = true;
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
