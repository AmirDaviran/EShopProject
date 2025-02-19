using EShop.Domain.Entities.Colors;
using EShop.Domain.Enums.ColorEnums;
using EShop.Domain.ViewModels.Colors;
using EShop.Domain.ViewModels.Colors.Product_Color;
using EShop.Domain.ViewModels.Products;

namespace EShop.Application.Interfaces
{
    public interface IColorService
    {
        #region Color CRUD
        Task<Color> GetColorByColorId(int colorId);
        Task<List<ColorListsViewModel>> GetAllColorsList();
        Task<List<Color>> GetAllColors();
        Task<CreateColorResult> CreateColor(CreateColorViewModel createColor);
        Task<EditColorResult> EditColor(EditColorViewModel editColor);
        Task<bool> DeleteColor(int colorId);
        #endregion

        #region Color Product Mapping CRUD
        Task<ProductColorViewModel> GetProductColorMapping(int productColorMappingId);
        Task<List<ProductColorMappingListViewModel>> GetAllProductColorLists();
        Task<CreateProductColorMappingResult> CreateProductColorMapping(CreateProductColorMappingViewModel mappingViewModel);
        Task<EditProductColorMappingResult> EditProductColorMapping(EditProductColorMappingViewModel mappingViewModel);
        Task<bool> DeletProductColorMapping(int  colorMappingId);
        #endregion
        Task SaveChanges();
    }
}
