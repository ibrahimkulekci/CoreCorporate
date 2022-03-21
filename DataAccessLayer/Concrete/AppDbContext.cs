using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class AppDbContext: DbContext
    {

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseSqlServer("server=DESKTOP-P79UT4Q;database=CoreCorporateDb;integrated security=true;");
            }
        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
