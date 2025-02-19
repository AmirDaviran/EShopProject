using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Enums.CategoryEnums;
using EShop.Domain.ViewModels.Products;
namespace EShop.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAllCategoriesAsync();
        Task<List<Category>> GetAllCategoriesInForm();
        Task<CategoryViewModel?> GetCategoryByIdAsync(int id);
        Task<CreateCategoryResult> CreateCategoryAsync(CreateCategoryViewModel model);
        Task<EditCategoryResult> EditCategoryAsync(EditCategoryViewModel model);
        Task<DeleteCategoryResult> DeleteCategoryAsync(int id);

        #region ClientSide

        Task<List<CategoryViewModel>> GetMainCategoriesAsync();


        #endregion
    }
}