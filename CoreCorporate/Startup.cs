using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AppDbConnectionString")));

            services.AddTransient(typeof(IGenericDal<>), typeof(GenericRepository<>));
            services.AddTransient<IGalleryRepository, GalleryRepository>();
            //services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddTransient<IGalleryService, GalleryService>();

            services.AddTransient<IGalleryImageRepository, GalleryImageRepository>();
            services.AddTransient<IGalleryImageService, GalleryImageService>();

            services.AddTransient<IMenuRepository, EfMenuRepository>();
            services.AddTransient<IMenuService, MenuService>();

            services.AddTransient<ISliderRepository, SliderRepository>();
            services.AddTransient<ISliderService, SliderService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<ICatalogDal, EfCatalogRepository>();
            services.AddTransient<ICatalogService, CatalogManager>();

            services.AddTransient<ITeamDal, EfTeamRepository>();
            services.AddTransient<ITeamService, TeamManager>();

            services.AddTransient<IPartnerDal, EfPartnerRepository>();
            services.AddTransient<IPartnerService, PartnerManager>();

            services.AddTransient<IServiceDal, EfServiceRepository>();
            services.AddTransient<IServiceService, ServiceManager>();

            services.AddTransient<IProductDal, EfProductRepository>();
            services.AddTransient<IProductService, ProductManager>();

            services.AddTransient<IProductCategoryDal, EfProductCategoryRepository>();
            services.AddTransient<IProductCategoryService, ProductCategoryManager>();

            services.AddTransient<INewsDal, EfNewsRepository>();
            services.AddTransient<INewsService, NewsManager>();

            services.AddTransient<IContactRepository, EfContactRepository>();
            services.AddTransient<IContactService, ContactService>();

            services.AddTransient<ISettingRepository, EfSettingRepository>();
            services.AddTransient<ISettingService, SettingService>();

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddMvc()
                .AddSessionStateTempDataProvider();
            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x => {
                    x.LoginPath = "/AdminPanel/User/Login";
                });               

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseSession();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
