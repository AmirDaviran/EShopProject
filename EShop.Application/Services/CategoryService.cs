using EShop.Application.Interfaces;
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Enums.CategoryEnums;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Products;

namespace EShop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        #region Fields

        private readonly ICategoryRepository _categoryRepository;

        #endregion

        #region Constructor

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        #endregion 


        #region Methods

        #region CreateCategoryAsync
        public async Task<CreateCategoryResult> CreateCategoryAsync(CreateCategoryViewModel model)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            if (categories.Any(c => c.Title.Equals(model.Title, StringComparison.OrdinalIgnoreCase) && !c.IsDeleted))
                return CreateCategoryResult.DuplicateTitle;

            var category = new Category()
            {
                Title = model.Title,
                ParentCategoryId = model.ParentCategoryId,
                DisplayOrder = model.DisplayOrder,
                CreatedDate = DateTime.Now,
                IsDeleted = false

            };

            await _categoryRepository.AddCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();

            if (category.Id > 0)
                return CreateCategoryResult.Success;
            else
                return CreateCategoryResult.Error;

        }

        #endregion

        #region DeleteCategoryAsync

        public async Task<DeleteCategoryResult> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null || category.IsDeleted)
                return DeleteCategoryResult.NotFound;

            if (category.SubCategories != null && category.SubCategories.Any(sc => !sc.IsDeleted))
                return DeleteCategoryResult.HasSubCategories;

            await _categoryRepository.SoftDeleteCategoryAsync(id);
            await _categoryRepository.SaveChangesAsync();

            return DeleteCategoryResult.Success;

        }

        #endregion

        #region EditCategoryAsync
        public async Task<EditCategoryResult> EditCategoryAsync(EditCategoryViewModel model)
        {
            var category=await _categoryRepository.GetCategoryByIdAsync(model.Id);

            if(category==null || category.IsDeleted)
                return EditCategoryResult.NotFound;

            var categories=await _categoryRepository.GetAllCategoriesAsync();
            if (categories.Any(c => c.Id != model.Id && c.Title.Equals(model.Title, StringComparison.OrdinalIgnoreCase) && !c.IsDeleted))
                return EditCategoryResult.DuplicatedTitle;

            category.Title = model.Title;
            category.ParentCategoryId = model.ParentCategoryId;
            category.DisplayOrder = model.DisplayOrder;

            await _categoryRepository.UpdateCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();


            if (category.Id > 0)
                return EditCategoryResult.Success;
            else
                return EditCategoryResult.Failure;
        }

        #endregion



        private List<CategoryViewModel> GetSubCategoryViewModels(IEnumerable<Category> subCategories)
        {
            return subCategories
                .Where(sc => !sc.IsDeleted)
                .OrderBy(sc => sc.DisplayOrder)
                .Select(sc => new CategoryViewModel
                {
                    Id = sc.Id,
                    Title = sc.Title,
                    ParentCategoryId = sc.ParentCategoryId,
                    ParentCategoryTitle = sc.ParentCategory != null ? sc.ParentCategory.Title : string.Empty,
                    DisplayOrder = sc.DisplayOrder,
                    SubCategories = GetSubCategoryViewModels(sc.SubCategories)
                })
                .ToList();
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            // انتخاب دسته‌بندی‌های سطح بالا (بدون والد)
            var topLevelCategories = categories
                .Where(c => c.ParentCategoryId == null)
                .OrderBy(c => c.DisplayOrder)
                .ToList();

            var viewModelList = new List<CategoryViewModel>();

            foreach (var category in topLevelCategories)
            {
                var viewModel = new CategoryViewModel
                {
                    Id = category.Id,
                    Title = category.Title,
                    ParentCategoryId = category.ParentCategoryId,
                    ParentCategoryTitle = category.ParentCategory != null ? category.ParentCategory.Title : string.Empty,
                    DisplayOrder = category.DisplayOrder,
                    SubCategories = GetSubCategoryViewModels(category.SubCategories)
                };

                viewModelList.Add(viewModel);
            }

            return viewModelList;
        }

        public async Task<CategoryViewModel?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null || category.IsDeleted)
                return null;

            // تبدیل موجودیت به ویومدل به‌صورت درون‌خطی
            var viewModel = new CategoryViewModel
            {
                Id = category.Id,
                Title = category.Title,
                ParentCategoryId = category.ParentCategoryId,
                ParentCategoryTitle = category.ParentCategory != null ? category.ParentCategory.Title : string.Empty,
                DisplayOrder = category.DisplayOrder,
                SubCategories = GetSubCategoryViewModels(category.SubCategories)
            };

            return viewModel;
        }

        public async Task<List<CategoryViewModel>> GetMainCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            var mainCategories = categories
                .Where(c => c.ParentCategoryId == null && !c.IsDeleted)
                .OrderBy(c => c.DisplayOrder)
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    ParentCategoryId = c.ParentCategoryId,
                    ParentCategoryTitle = string.Empty, // چون دسته‌بندی سطح بالا والد نداره
                    DisplayOrder = c.DisplayOrder,
                    SubCategories = GetSubCategoryViewModels(c.SubCategories)
                })
                .ToList();

            return mainCategories;
        }

        public async Task<List<Category>> GetAllCategoriesInForm()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        #endregion
    }
}
