using EShop.Domain.Entities.Colors;
using EShop.Domain.Entities.ProductEntity;

namespace EShop.Domain.Interfaces
{
    public interface IColorRepository
    {
        #region Color CRUD
        Task<Color> GetColorById(int colorId);
        Task<List<Color>> GetAllColors();
        Task<Color?> GetColorByCode(string code);
        Task<Color?> GetColorByName(string name);
        Task UpdateColor(Color color);
        Task<bool> DeleteColor(int colorId);
        Task AddColor(Color color);
        #endregion

        #region Color Product Mapping CRUD
        Task<ProductColorMapping?> GetProductColorMappingById (int id);
        Task<List<ProductColorMapping>> GetAllProductColorMappings();
        Task<List<ProductColorMapping>> GetProductColorMappingsById(int ProductId);
        Task AddProductColorMapping(ProductColorMapping productColorMapping);
        Task UpdateProductColorMapping(ProductColorMapping productColorMapping);
        Task<bool> DeleteProductColorMapping(int id);
        #endregion

        #region Save Changes
        Task SaveChanges();
        #endregion
    }
}
