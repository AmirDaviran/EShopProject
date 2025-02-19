using EShop.Domain.Entities.Colors;
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly EShopDbContext _context;
        public ColorRepository(EShopDbContext context)
        {
            _context = context;
        }


        #region Color CRUD
        public async Task<Color> GetColorById(int colorId)
        {
            var color = await _context.Colors.FirstOrDefaultAsync(c => c.Id == colorId);
            return color;
        }
        public async Task<Color?> GetColorByCode(string code)
        {
            return await _context.Colors.FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task<Color?> GetColorByName(string name)
        {
            return await _context.Colors.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<List<Color>> GetAllColors()
        {
            return await _context.Colors.Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task UpdateColor(Color color)
        {
            _context.Colors.Update(color);
        }

        public async Task<bool> DeleteColor(int colorId)
        {
            var color = await GetColorById(colorId);
            if (color != null)
            {
                color.IsDeleted = true;
                await SaveChanges();
                return true;
            }
            return false;
        }

        public async Task AddColor(Color color)
        {
            await _context.Colors.AddAsync(color);
        }


        #endregion

        #region Product Color Mapping CRUD
        public async Task<ProductColorMapping?> GetProductColorMappingById(int id)
        {
            return await _context.ProductColorMappings.FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<List<ProductColorMapping>> GetAllProductColorMappings()
        {
            return await _context.ProductColorMappings
                .Include(c => c.Color)
                .Include(pc => pc.Product)
                .ToListAsync();
        }

        public async Task AddProductColorMapping(ProductColorMapping productColorMapping)
        {
            await _context.ProductColorMappings.AddAsync(productColorMapping);
        }

        public async Task UpdateProductColorMapping(ProductColorMapping productColorMapping)
        {
             _context.ProductColorMappings.Update(productColorMapping);
        }

        public async Task<bool> DeleteProductColorMapping(int id)
        {
            var productColorMapping = await GetProductColorMappingById(id);
            if (productColorMapping != null)
            {
                productColorMapping.IsDeleted = true;
                await SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region Save Changes

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductColorMapping>> GetProductColorMappingsById(int ProductId)
        {
            return await _context.ProductColorMappings
                .Where(p => p.Id == ProductId)
                .ToListAsync();
        }

        #endregion
    }
}
