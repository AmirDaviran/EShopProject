using EShop.Domain.Entities.Account;
using EShop.Domain.Entities.Colors;
using EShop.Domain.Entities.ContactUs;
using EShop.Domain.Entities.FAQ;
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Entities.Specifications;
using EShop.Domain.Entities.TicketSystem;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Context
{
    public class EShopDbContext : DbContext
    {
        public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options)
        {

        }

        #region User
        public DbSet<User> Users { get; set; }
        #endregion

        #region ContactUs
        public DbSet<ContactUs> ContactUs { get; set; }
        #endregion

        #region Ticket System
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }
       #endregion

        #region Product

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        public DbSet<ProductColorMapping> ProductColorMappings { get; set; }
        public DbSet<CategorySpecificationMapping> CategorySpecificationMappings { get; set; }
        public DbSet<ProductSpecificationMapping> ProductSpecificationMappings { get; set; }

        #endregion

        #region Specification
        public DbSet<Specification> Specifications { get; set; }
        #endregion

        #region FAQ
        public DbSet<FAQs> FAQs { get; set; }
        #endregion

        #region Color
        public DbSet<Color> Colors { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var item in entity.GetForeignKeys())
                {
                    item.DeleteBehavior = DeleteBehavior.NoAction;
                }
            }
            base.OnModelCreating(modelBuilder);
        }

    }
}
