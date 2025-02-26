using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.ViewModels.Products.Specification;

namespace EShop.Domain.Interfaces;

public interface ISpecificationRepository
{
    Task<Specification> GetSpecificationByIdAsync(int id);
    Task<FilterSpecificationViewModel> FilterAsync(FilterSpecificationViewModel model);
    Task InsertAsync(Specification specification);
    void Update(Specification specification);
    Task SaveAsync();
}