using EShop.Domain.Entities.ProductEntity;
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
    public class CategorySpesificationRepository : ICategorySpesificationRepository
    {
        #region Constructor
        private readonly EShopDbContext _context;
        public CategorySpesificationRepository(EShopDbContext context)
        {
            _context = context;
        }
        #endregion
        public async Task AddAsync(CategorySpecificationMapping categorySpecificationMapping)
        {
            await _context.AddAsync(categorySpecificationMapping);
        }

        public async Task<List<Specification>> GetSpecificationsByCategoryId(int categoryId)
        {
            return await _context.CategorySpecificationMappings
                .Where(m => m.CategoryId == categoryId)
                .Select(m => m.Specification)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
