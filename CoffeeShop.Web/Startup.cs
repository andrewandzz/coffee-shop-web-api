using CoffeeShop.Data;
using CoffeeShop.Data.Interfaces;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Logics.Interfaces;
using CoffeeShop.Logics.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoffeeShop.Web
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
            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);

            services.AddDbContextPool<ShopDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("CoffeeShopDb"),
                    options => options.MigrationsAssembly("CoffeeShop.Data")
                )
            );

            services.AddAutoMapper(config =>
            {
                config.AddProfile(typeof(Web.Mapping.MappingProfile));
                config.AddProfile(typeof(Logics.Mapping.MappingProfile));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICoffeeRepository, CoffeeRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<ICoffeeService, CoffeeService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();

            services.AddSpaStaticFiles(options => options.RootPath = "ClientApp/dist");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(builder =>
            {
                builder.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    builder.UseAngularCliServer("start");
                }
            });
        }
    }
}
