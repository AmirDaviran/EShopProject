using EShop.Application.Interfaces;
using EShop.Application.Utilities.Extensions;
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Enums.ColorEnums;
using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Colors;
using EShop.Domain.ViewModels.Colors.Product_Color;
using EShop.Domain.ViewModels.Products;
using EShop.Domain.ViewModels.Products.Site_Side;

namespace EShop.Application.Services
{
    public class ProductService
            (IProductRepository _productRepository,
            ICategoryService _categoryService,
            ICategoryRepository _categoryRepository,
            IColorRepository _colorRepository)
        : IProductService
    {

        #region Get Product By Id
        public async Task<Product> GetProductByProductId(int productId)
        {
            return await _productRepository.GetProductById(productId);
        }
        #endregion

        #region Get All Products
        public async Task<List<ProductListViewModel>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            List<ProductListViewModel> productLists = new List<ProductListViewModel>();

            foreach (var product in products)
            {
                productLists.Add(new ProductListViewModel
                {
                    productId = product.Id,
                    Price = product.Price,
                    CreatedDate = product.CreatedDate,
                    ImageName = product.ImageName

                });
            }
            return productLists;
        }
        #endregion

        #region Create Product 
        public async Task<CreateProductResult> CreateProduct(CreateProductViewModel createProduct)
        {
            var product = new Product()
            {
                PersianTitle = createProduct.PersianTitle,
                EnglishTitle = createProduct.EnglishTitle,
                Review = createProduct.Review,
                ExpertReview = createProduct.ExpertReview,
                Price = createProduct.Price,
                CreatedDate = DateTime.Now,
            };

            if (createProduct.ImageFile != null && createProduct.ImageFile.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(createProduct.ImageFile.FileName);
                createProduct.ImageFile.AddImageToTheServer(imageName, PathExtensions.ProductOriginServer, 215, 215, PathExtensions.ProductThumbServer);
                product.ImageName = imageName;
            }
            else
            {
                return CreateProductResult.NotImage;
            }

            await _productRepository.AddProduct(product);
            await _productRepository.SaveChanges();
            await _categoryRepository.AddProductSelectedCategories(createProduct.SelectedCategories, product.Id);
            //await _productRepository.AddProductColorMappings(createProduct.SelectedColors, product.Id);

            return CreateProductResult.Success;
        }


        #endregion

        #region Update Product
        public async Task UpdateProduct(Product product)
        {
            await _productRepository.UpdateProduct(product);
        }
        #endregion

        #region Delete Product
        public async Task<bool> DeleteProduct(int productId)
        {
            return await _productRepository.DeleteProductById(productId);
        }


        #endregion

        public async Task<List<ProductSelectedCategory>> GetAllProductCategories()
        {
            return await _categoryRepository.GetAllProductCategoriesAsync();
        }

        public async Task<EditProductViewModel> GetProductForEdit(int productId)
        {
            var product = await _productRepository.GetProductById(productId);

            if (product != null)
            {
                return new EditProductViewModel
                {
                    PersianTitle = product.PersianTitle,
                    EnglishTitle = product.EnglishTitle,
                    Review = product.Review,
                    ExpertReview = product.ExpertReview,
                    Price = product.Price,
                    ImageName = product.ImageName,
                    SelectedCategories = await _categoryRepository.GetAllProductCategoriesId(productId)
                };

            }
            return null;
        }

        public async Task<EditProductResult> EditProduct(EditProductViewModel editProduct)
        {
            var product = await _productRepository.GetProductById(editProduct.ProductId);
            if (product != null)
            {
                product.PersianTitle = editProduct.PersianTitle;
                product.EnglishTitle = editProduct.EnglishTitle;
                product.Review = editProduct.Review;
                product.ExpertReview = editProduct.ExpertReview;
                product.Price = editProduct.Price;
                if (editProduct.ImageFile != null && editProduct.ImageFile.IsImage())
                {
                    var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(editProduct.ImageFile.FileName);
                    editProduct.ImageFile.AddImageToTheServer(imageName, PathExtensions.ProductOriginServer, 215, 215, PathExtensions.ProductThumbServer);
                    product.ImageName = imageName;
                }
                await _productRepository.UpdateProduct(product);
                await _categoryRepository.RemoveProductSelectedCategories(editProduct.ProductId);
                await _categoryRepository.AddProductSelectedCategories(editProduct.SelectedCategories, editProduct.ProductId);

                await _productRepository.SaveChanges();
                return EditProductResult.Success;
            }
            else
            {
                return EditProductResult.NotFound;
            }
        }

        #region ProductColorMapping
        public async Task<AddProductColorResult> AddColorToProduct(AddProductColorViewModel model)
        {
            var product = await GetProductByProductId(model.ProductId);
            if (product == null)
            {
                return AddProductColorResult.NotFound;
            }
            //product.Price += model.DisparityAmount;

            var mapping = new ProductColorMapping
            {
                Amount = model.DisparityAmount,
                ColorId = model.SelectedColorId,
                ProductId = model.ProductId,
                CreatedDate = DateTime.Now
            };

            await _colorRepository.AddProductColorMapping(mapping);
            //await UpdateProduct(product);
            await _colorRepository.SaveChanges();

            return AddProductColorResult.Success;

        }

        public async Task<List<GetProductColorMappingsViewModel>> GetProductColorMappings(int productId)
        {
            var product = await _productRepository.GetProductById(productId);
            if (product != null)
            {
                var colorMappings = await _colorRepository.GetProductColorMappingsById(productId);
                var result = colorMappings.Select(mapping => new GetProductColorMappingsViewModel

                {
                    ProductId = mapping.ProductId,
                    Name = mapping.Color.Name,
                    Code = mapping.ColorId,
                    Price = product.Price + mapping.Amount
                }).ToList();
                return result;
            }
            return new List<GetProductColorMappingsViewModel>();
        }

        public async Task<List<ProductWithColorsViewModel>> GetAllProductsWithColorsAndPrices()
        {
            var products = await _productRepository.GetAllProducts();
            return products.Select(product => new ProductWithColorsViewModel
            {
                ProductId = product.Id,
                ImageName = product.ImageName,
                Price = product.Price,
                CreatedDate = product.CreatedDate,
                ColorMappings = product.ProductColorMappings
                .Where(mapping => !mapping.IsDeleted)
                .Select(mapping => new ColorMappingViewModel
                {
                    Id = mapping.Id,
                    ColorId = mapping.ColorId,
                    ColorName = mapping.Color.Name,
                    CreatedDate = mapping.CreatedDate,
                    //Price = product.Price + mapping.Amount
                    Price = mapping.Amount
                }).ToList()

            }).ToList();
        }

        #endregion

        #region Site Side
        public async Task<ProductDetailsViewModel> ShowProductDetails(int productId)
        {
            var product = await _productRepository.ShowProductDetails(productId);
            if (product != null)
            {
                return new ProductDetailsViewModel
                {
                    ProductId = product.Id,
                    PersianTile = product.PersianTitle,
                    EnglishTitle = product.EnglishTitle,
                    Review = product.Review,
                    ExpertReview = product.ExpertReview,
                    Price = product.Price,
                    ImageName = product.ImageName,
                    Category = product.ProductSelectedCategories.FirstOrDefault().Category
                };
            }
            return null;
        }






        #endregion

    }
}
