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
            services.AddScoped<IFAQRepository, FAQRepository>();
            services.AddScoped<IFAQCategoryRepository, FAQCategoryRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            #endregion


            #region Services Registration
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IFAQService, FAQService>();
            services.AddScoped<IFAQCategoryService, FAQCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            #endregion


            #region Tools
            services.AddTransient<IViewRenderService, RenderViewToString>();
            #endregion


        }
    }
}
