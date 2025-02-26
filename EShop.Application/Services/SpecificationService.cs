using EShop.Application.Interfaces;
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.Enums.SpecificationEnum;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Products.Product;
using EShop.Domain.ViewModels.Products.Specification;

namespace EShop.Application.Services;

public class SpecificationService(ISpecificationRepository _specificationRepository) : ISpecificationService
{
    #region Create

    public async Task<CreateSpecificationResult> CreateAsync(CreateSpecificationViewModel model)
    {
        #region Validation

        if (model == null || string.IsNullOrWhiteSpace(model.Name))
        {
            return CreateSpecificationResult.InvalidInput;
        }

        #endregion


        Specification specification = new Specification()
        {
            CreatedDate = DateTime.Now,
            Name = model.Name
        };

        await _specificationRepository.InsertAsync(specification);
        await _specificationRepository.SaveAsync();

        return CreateSpecificationResult.Success;
    }

    #endregion

    #region Delete

    public async Task<DeleteSpecificationResult> DeleteAsync(int id)
    {
        var spec = await _specificationRepository.GetSpecificationByIdAsync(id);

        if (spec == null || spec.IsDeleted)
        {
            return DeleteSpecificationResult.NotFound;
        }

        spec.IsDeleted = true;

        _specificationRepository.Update(spec);
        await _specificationRepository.SaveAsync();

        return DeleteSpecificationResult.Success;
    }

    #endregion

    #region Filter

    public async Task<FilterSpecificationViewModel> FilterAsync(FilterSpecificationViewModel model)
    {
        return await _specificationRepository.FilterAsync(model);
    }

    #endregion

    #region Update

    public async Task<UpdateSpecificationResult> UpdateAsync(UpdateSpecificationViewModel model)
    {
        if (model == null || model.Id <= 0)
        {
            return UpdateSpecificationResult.InvalidInput;
        }

        var spec = await _specificationRepository.GetSpecificationByIdAsync(model.Id);
        if (spec == null)
        {
            return UpdateSpecificationResult.NotFound;
        }

        spec.Name = model.Name;

        _specificationRepository.Update(spec);
        await _specificationRepository.SaveAsync();

        return UpdateSpecificationResult.Success;
    }

    #endregion

    #region GetForUpdate

    public async Task<UpdateSpecificationViewModel> GetForUpdateAsync(int id)
    {
        var spec = await _specificationRepository.GetSpecificationByIdAsync(id);
        if (spec == null)
        {
            return null;
        }
        else
        {
            return new UpdateSpecificationViewModel()
            {
                Id = spec.Id,
                Name = spec.Name
            };
        }
    }
    #endregion
}