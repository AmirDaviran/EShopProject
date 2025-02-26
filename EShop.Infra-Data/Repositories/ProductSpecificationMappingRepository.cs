using EShop.Domain.Entities.ProductEntity.Mapping;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Repositories
{
    public class ProductSpecificationMappingRepository : IProductSpecificationMappingRepository
    {
        private readonly EShopDbContext _context;

        public ProductSpecificationMappingRepository(EShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductSpecificationMapping>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductSpecificationMappings
                .Where(psm => psm.ProductId == productId && !psm.IsDeleted)
                .Include(psm => psm.Specification)
                .ToListAsync();
        }

        public async Task<ProductSpecificationMapping> GetByIdAsync(int id)
        {
            return await _context.ProductSpecificationMappings
                .Where(psm => psm.Id == id && !psm.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task InsertAsync(ProductSpecificationMapping mapping)
        {
            await _context.ProductSpecificationMappings.AddAsync(mapping);
        }

        public void Update(ProductSpecificationMapping mapping)
        {
            _context.ProductSpecificationMappings.Update(mapping);
        }

        public async Task DeleteAsync(int id)
        {
            var mapping = await GetByIdAsync(id);
            if (mapping != null)
            {
                mapping.IsDeleted = true;
                _context.ProductSpecificationMappings.Update(mapping);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}