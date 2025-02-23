using EShop.Application.Interfaces;
using EShop.Domain.Entities.Colors;
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Enums.ColorEnums;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Colors;
using EShop.Domain.ViewModels.Colors.Product_Color;
using EShop.Domain.ViewModels.Products;

namespace EShop.Application.Services
{
    public class ColorService(IColorRepository _colorRepository) : IColorService
    {

        #region Color CRUD
        public async Task<Color> GetColorByColorId(int colorId)
        {
            return await _colorRepository.GetColorById(colorId);
        }

        public async Task<List<ColorListsViewModel>> GetAllColorsList()
        {
            var colors = await _colorRepository.GetAllColors();
            List<ColorListsViewModel> list = new List<ColorListsViewModel>();
            foreach (var color in colors)
            {
                list.Add(new ColorListsViewModel
                {
                    ColorId = color.Id,
                    Code = color.Code,
                    Name = color.Name,
                    CreatedDate = color.CreatedDate,
                });
            }
            return list;
        }
        public async Task<List<Color>> GetAllColors()
        {
          return  await _colorRepository.GetAllColors();
        }

        public async Task<CreateColorResult> CreateColor(CreateColorViewModel createColor)
        {
            var color = new Color()
            {
                Name = createColor.Name,
                Code = createColor.Code,
                CreatedDate = DateTime.Now
            };
            await _colorRepository.AddColor(color);
            await SaveChanges();

            return CreateColorResult.Success;
        }

        public async Task<EditColorResult> EditColor(EditColorViewModel editColor)
        {
            var color = await _colorRepository.GetColorById(editColor.ColorId);
            if (color != null)
            {
                color.Code = editColor.Code;
                color.Name = editColor.Name;
                await _colorRepository.UpdateColor(color);
                await SaveChanges();
                return EditColorResult.Success;
            }
            return EditColorResult.NotFound;
        }

        public async Task<bool> DeleteColor(int colorId)
        {
            return await _colorRepository.DeleteColor(colorId);
        }


        #endregion

        #region Product Color Mapping CRUD
        public async Task<ProductColorViewModel> GetProductColorMapping(int productColorMappingId)
        {
            var colorMapping = await _colorRepository.GetProductColorMappingById(productColorMappingId);
            ProductColorViewModel model = new ProductColorViewModel()
            {
                Amount = colorMapping.Amount,
                ColorId = colorMapping.ColorId,
                ProductId = colorMapping.ProductId
            };
            return model;
        }

        public async Task<List<ProductColorMappingListViewModel>> GetAllProductColorLists()
        {
            var productColors = await _colorRepository.GetAllProductColorMappings();
            List<ProductColorMappingListViewModel> models = new List<ProductColorMappingListViewModel>();
            foreach(var productColor in  productColors)
            {
                models.Add(new ProductColorMappingListViewModel()
                {
                    ProductId = productColor.ProductId,
                    Code = productColor.Color.Code,
                    ProductName = productColor.Product.PersianTitle,
                    CreatedDate = productColor.CreatedDate, 
                    MappingId = productColor.Id
                });
            }
            return models;
        }

        public async Task<CreateProductColorMappingResult> CreateProductColorMapping(CreateProductColorMappingViewModel mappingViewModel)
        {
            var mapping = new ProductColorMapping
            {
                Amount = mappingViewModel.Amount,
                ColorId = mappingViewModel.SelectedColorId,
                ProductId = mappingViewModel.ProductId,
                CreatedDate = DateTime.Now
            };
            await _colorRepository.AddProductColorMapping(mapping);
            await SaveChanges();
            return CreateProductColorMappingResult.Success;
        }

        public async Task<EditProductColorMappingResult> EditProductColorMapping(EditProductColorMappingViewModel mappingViewModel)
        {
            var mapping = await _colorRepository.GetProductColorMappingById(mappingViewModel.MappingId);
            if(mapping != null)
            {
                mapping.Amount = mappingViewModel.Amount;
                mapping.ColorId = mappingViewModel.SelectedColorId;
                await _colorRepository.UpdateProductColorMapping(mapping);
                await SaveChanges();
                return EditProductColorMappingResult.Success;
            }
            return EditProductColorMappingResult.NotFound;
        }

        public async Task<bool> DeletProductColorMapping(int colorMappingId)
        {
            return await _colorRepository.DeleteProductColorMapping(colorMappingId);
        }
        #endregion


        #region Save Changes
        public async Task SaveChanges()
        {
            await _colorRepository.SaveChanges();
        }

       
        #endregion
    }
}
