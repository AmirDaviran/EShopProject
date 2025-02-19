using EShop.Domain.Entities.Specifications;
using EShop.Domain.Enums.SpesificationEnums;
using EShop.Domain.ViewModels.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Interfaces
{
    public interface ISpecificationService
    {
        Task<Specification?> GetByIdAsync(int id);
        Task<List<Specification>> GetAllAsync();
        Task<AddSpecificationResult> AddAsync (AddSpecificationViewModel model);
        Task UpdateAsync (Specification specification);
        Task<bool> DeleteAsync (int id);

        Task SaveChangesAsync();
    }
}


