using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Products.Product;
using EShop.Domain.ViewModels.Products.Product.ClientSide;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;


namespace EShop.Infra_Data.Repositories
{
    public class ProductRepository(EShopDbContext _context) : IProductRepository
    {
        #region AdminSide

        #region GetAll
        public async Task<List<ProductListViewModel>> GetAllAsync()
        {
            return await _context.Products
                .Where(product => !product.IsDeleted)
                .Select(product => new ProductListViewModel
                {
                    Id = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    ImageName = product.ImageName,
                    CreatedDate = product.CreatedDate
                })
                .ToListAsync();
        }

        #endregion

        #region GetById
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync();
        }

        #endregion

        #region Insert

        public async Task InsertAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        #endregion

        #region SaveAsync
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Update

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        #endregion

        #region Filter

        public async Task<FilterProductViewModel> FilterAsync(FilterProductViewModel model)
        {
            var query = _context.Products
                .Where(product => !product.IsDeleted)
                .Include(product => product.ProductCategoryMappings)
                .ThenInclude(pcm => pcm.Category)
                .AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                query = query.Where(product => EF.Functions.Like(product.Title, $"%{model.SearchTerm}%"));
            }

            #endregion

            #region Paging
            await model.Paging(query.Select(product => new ProductListViewModel
            {
                CreatedDate = product.CreatedDate,
                Id = product.Id,
                ImageName = product.ImageName,
                Price = product.Price,
                Title = product.Title,
                CategoryTitle = product.ProductCategoryMappings.Any()
                    ? product.ProductCategoryMappings.First().Category.Title
                    : null // شرط ساده به جای ?.
            }));
            #endregion

            return model;
        }

        #endregion

        #region GetProductsByCategoryId

        public async Task<List<ProductListViewModel>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.ProductCategoryMappings
                .Where(psc => psc.CategoryId == categoryId && !psc.IsDeleted) // کامنت: فیلتر بر اساس CategoryId
                .Select(psc => new ProductListViewModel
                {
                    Id = psc.Product.Id,
                    Title = psc.Product.Title,
                    Price = psc.Product.Price,
                    ImageName = psc.Product.ImageName,
                    CreatedDate = psc.Product.CreatedDate
                })
                .ToListAsync();
        }

        #endregion

        #endregion

        #region ClientSide

        #region GetMyProductData
        public async Task<Product> GetMyProductDataAsync(int productId)
        {
            return await _context.Products
                .Include(p => p.ProductSpecificationMappings)
                .ThenInclude(psm => psm.Specification)
                .Include(p => p.ProductCategoryMappings)
                .ThenInclude(pcm => pcm.Category)
                .ThenInclude(c => c.ParentCategory)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        #endregion

        #region Filter
        public async Task<FilterClientSideProductViewModel> FilterClientAsync(FilterClientSideProductViewModel model)
        {
            var query = _context.Products
                .Where(product => !product.IsDeleted)
                .AsQueryable();

            #region فیلتر جستجو
            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                query = query.Where(product => EF.Functions.Like(product.Title, $"%{model.SearchTerm}%"));
            }
            #endregion

            #region مرتب‌سازی
            switch (model.SortBy)
            {
                               case "cheapest":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "most-expensive":
                    query = query.OrderByDescending(p => p.Price);
                    break;
                default:
                    query = query.OrderByDescending(p => p.CreatedDate);
                    break;
            }
            #endregion

            #region صفحه‌بندی
            await model.Paging(query.Select(product => new ProducClientSidetListViewModel
            {
                Id = product.Id,
                Image = product.ImageName,
                Price = product.Price,
                Title = product.Title,
                CreatedDate = product.CreatedDate
            }));
            #endregion

            return model;
        }

        #endregion

        #endregion
    }
}
