using EShop.Domain.Entities.ProductEntity.Mapping;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Products.Product_Specification;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Repositories
{
    public class ProductSpecificationMappingRepository(EShopDbContext _context) : IProductSpecificationMappingRepository
    {
        

        public async Task<List<ProductSpecificationListViewModel>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductSpecificationMappings
                .Where(psm => psm.ProductId == productId && !psm.IsDeleted)
                .Include(psm => psm.Specification)
                .Select(psm => new ProductSpecificationListViewModel
                {
                    MappingId = psm.Id,
                    SpecificationId = psm.SpecificationId,
                    SpecificationName = psm.Specification.Name,
                    Value = psm.Value
                })
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
            var mapping = await _context.ProductSpecificationMappings
                .FirstOrDefaultAsync(psm => psm.Id == id && !psm.IsDeleted);
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

        #region Filter

        public async Task<ProductSpecificationFilterViewModel> FilterAsync(ProductSpecificationFilterViewModel model)
        {
            var query = _context.ProductSpecificationMappings
                .Where(psm => !psm.IsDeleted && psm.ProductId==model.ProductId)
                .Include(spc => spc.Specification)
                .AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                query = query.Where(psm => EF.Functions.Like(psm.Value, $"%{model.SearchTerm}%") ||
                                           EF.Functions.Like(psm.Specification.Name, $"%{model.SearchTerm}%"));
            }
            #endregion

            #region Paging

            await model.Paging(query.Select(psm => new ProductSpecificationListViewModel()
            {
                Value = psm.Value,
                MappingId = psm.Id,
                SpecificationId = psm.SpecificationId,
                SpecificationName = psm.Specification.Name
            }));

            #endregion

            return model;
        }

        #endregion
    }
}