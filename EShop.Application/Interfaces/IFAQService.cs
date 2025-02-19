using EShop.Domain.ViewModels.FAQ;
using EShop.Domain.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Interfaces
{
    public interface IFAQService
    {
        #region Admin Side

        //list
        Task<List<FAQViewModel>> GetAllAsync();

        //Create
        Task<bool> CreateAsync(CreateFAQViewModel model);

        //Update 
        Task<UpdateFAQViewModel> GetForUpdateAsync(int id);

        Task<bool> UpdateAsync(UpdateFAQViewModel model);

        //Delete
        Task<bool> DeleteAsync(int id);



        #endregion

        #region Client Side
        Task<List<FAQViewModel>> GetFAQAsync();
        #endregion

    }
}
