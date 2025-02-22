using EShop.Application.Interfaces;
using EShop.Application.Services;
using EShop.Application.Utilities.Convertors;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace EShop.IOC
{
    public class DependencyContainer
    {
        public static void RegisterService(IServiceCollection services)
        {
            #region Repositories Registration
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFAQRepository, FAQRepository>();
            services.AddScoped<IFAQCategoryRepository, FAQCategoryRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ISpecificationRepository, SpecificationRepository>();
            services.AddScoped<ICategorySpesificationRepository, CategorySpesificationRepository>();
            
            #endregion


            #region Services Registration
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFAQService, FAQService>();
            services.AddScoped<IFAQCategoryService, FAQCategoryService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<ISpecificationService, SpecificationService>();
            #endregion


            #region Tools
            services.AddTransient<IViewRenderService, RenderViewToString>();
            #endregion


        }
    }
}
