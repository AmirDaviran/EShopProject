using EShop.Domain.Entities.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface ISpecificationRepository
    {
        Task<Specification?> GetByIdAsync(int id);
        Task<List<Specification>> GetAllAsync();
        Task AddAsync(Specification specification);
        Task UpdateAsync(Specification specification);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();

    }
}
