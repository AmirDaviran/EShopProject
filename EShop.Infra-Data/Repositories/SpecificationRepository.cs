using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Products.Specification;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EShop.Infra_Data.Repositories;

public class SpecificationRepository(EShopDbContext _context) : ISpecificationRepository
{
    public async Task<FilterSpecificationViewModel> FilterAsync(FilterSpecificationViewModel model)
    {
        var query = _context.Specifications
            .Where(spec => !spec.IsDeleted)
            .AsQueryable();

        #region Filter

        if (!string.IsNullOrEmpty(model.SearchTerm))
        {
            query = query.Where(spec => spec.Name.Contains(model.SearchTerm));
        }

        #endregion

        #region Paging

        await model.Paging(query.Select(spec => new SpecificationListViewModel()
        {
            Id = spec.Id,
            Name = spec.Name,
            CreateDate = spec.CreatedDate
        }));

        #endregion
        return model;
    }

    public async Task InsertAsync(Specification specification)
    {
        await _context.Specifications.AddAsync(specification);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(Specification specification)
    {
        _context.Specifications.Update(specification);
    }

    public async Task<Specification> GetSpecificationByIdAsync(int id)
    {
        return await _context.Specifications
            .Where(spec =>spec.Id==id && !spec.IsDeleted)
            .FirstOrDefaultAsync();
    }
}