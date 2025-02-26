using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Products.Product;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;


namespace EShop.Infra_Data.Repositories
{
    public class ProductRepository(EShopDbContext _contex) : IProductRepository
    {
        #region GetAll
        public async Task<List<Product>> GetAllAsync()
        {
            return await _contex.Products
                .Where(product => !product.IsDeleted)
                .Include(psm => psm.ProductSpecificationMappings)
                .ThenInclude(spec => spec.Specification)
                .ToListAsync();
        }

        #endregion

        #region GetById
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _contex.Products
                 .Where(p => p.Id == id && !p.IsDeleted)
               .FirstOrDefaultAsync();
        }

        #endregion

        #region Insert

        public async Task InsertAsync(Product product)
        {
            await _contex.Products.AddAsync(product);
        }

        #endregion

        #region SaveAsync
        public async Task SaveAsync()
        {
            await _contex.SaveChangesAsync();
        }

        #endregion

        #region Update

        public void Update(Product product)
        {
            _contex.Products.Update(product);
        }

        #endregion

        #region Filter

        public async Task<FilterProductViewModel> FilterAsync(FilterProductViewModel model)
        {
            var query = _contex.Products
                .Where(product => !product.IsDeleted)
              .AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(model.Title))
            {
                query = query.Where(product => EF.Functions.Like(product.Title, $"%{model.Title}%"));
            }

            #endregion

            #region Paging
            await model.Paging(query.Select(product => new ProductViewModel
            {
                CreatedDate = product.CreatedDate,
                ExpertReview = product.ExpertReview,
                Id = product.Id,
                ImageName = product.ImageName,
                Price = product.Price,
                Review = product.Review,
                Title = product.Title,
                TitleDescription = product.TitleDescription,
            }));
            #endregion

            return model;
        }

        #endregion

      
    }
}
