using EShop.Domain.Enums.SpecificationEnum;
using EShop.Domain.ViewModels.Products.Specification;

namespace EShop.Application.Interfaces;

public interface ISpecificationService
{
    Task<FilterSpecificationViewModel> FilterAsync(FilterSpecificationViewModel model);
    Task<CreateSpecificationResult> CreateAsync(CreateSpecificationViewModel model);
    Task<UpdateSpecificationViewModel> GetForUpdateAsync(int id);
    Task<UpdateSpecificationResult> UpdateAsync(UpdateSpecificationViewModel model);
    Task<DeleteSpecificationResult> DeleteAsync(int id);
    Task<List<SpecificationListViewModel>> GetAllAsync();
}