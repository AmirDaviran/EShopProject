using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Entities.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface ICategorySpesificationRepository
    {
        Task AddAsync(CategorySpecificationMapping categorySpecificationMapping);

        Task<List<Specification>> GetSpecificationsByCategoryId(int categoryId);

        Task SaveChangesAsync();
    }
}
