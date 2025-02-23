using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Enums;
using EShop.Domain.ViewModels.Products.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAllAsync();
        Task<UpdateProductViewModel> GetForUpdateAsync(int id);

        Task<CreateProductResult> CreateAsync(CreateProductViewModel model);
        Task<UpdateProductResult> UpdateAsync(UpdateProductViewModel model);
        Task DeleteAsync(int id);

    }
}
