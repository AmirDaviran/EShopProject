using EShop.Application.Interfaces;
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Entities.Specifications;
using EShop.Domain.Enums.SpesificationEnums;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services
{
    public class SpecificationService : ISpecificationService
    {
        #region Constructor
        private readonly ISpecificationRepository _specificationRepository;
        private readonly ICategorySpesificationRepository _categorySpesificationRepository;
        public SpecificationService(ISpecificationRepository specificationRepository,
                                    ICategorySpesificationRepository categorySpesificationRepository)
        {
            _specificationRepository = specificationRepository;
            _categorySpesificationRepository = categorySpesificationRepository;
        }


        #endregion

        public async Task<Specification?> GetByIdAsync(int id)
        {
            return await _specificationRepository.GetByIdAsync(id);
        }

        public async Task<List<Specification>> GetAllAsync()
        {
           return await _specificationRepository.GetAllAsync();
        }

      

        public async Task<AddSpecificationResult> AddAsync(AddSpecificationViewModel model)
        {
            // Validate the input (you can add more checks as needed)  
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return AddSpecificationResult.Failed; 
            }

            // Create the Specification entity from the ViewModel  
            var specification = new Specification
            {
                Name = model.Name,
                CreatedDate = DateTime.Now
            };
            await _specificationRepository.AddAsync(specification);
            await SaveChangesAsync();
            // Step 3: Link the specification to the selected categories in the junction table.  
            foreach (var categoryId in model.SelectedCategories)
            {
                var categorySpecification = new CategorySpecificationMapping
                {
                    CategoryId = categoryId,
                    SpecificationId = specification.Id
                };

                await _categorySpesificationRepository.AddAsync(categorySpecification);
                await _categorySpesificationRepository.SaveChangesAsync();
            }

            return AddSpecificationResult.Success;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _specificationRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(Specification specification)
        {
            await _specificationRepository.UpdateAsync(specification);
        }

        public async Task SaveChangesAsync()
        {
            await _specificationRepository.SaveChangesAsync();
        }
    }
}
