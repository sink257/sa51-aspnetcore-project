using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Team8CA.DataAccess;
using Team8CA.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Team8CA.Services;
using Stripe;

namespace Team8CA
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


            services.AddDbContext<AppDbContext>
                (opt => opt.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DBConn")));

            //services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            services.AddControllersWithViews();
            services.AddScoped<Team8CA.Models.Customer>();
            services.AddScoped<Team8CA.Models.Products>();
            services.AddScoped<Team8CA.Models.Session>();
            services.AddScoped<Team8CA.Models.ShoppingCart>();
            //services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<ShoppingCart>(x => ShoppingCart.GetCart(x));

            services.AddHttpContextAccessor();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession(); //to set session before user moves from 1 page to another (establish session before routing request)

            app.UseRouting();

            //StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Gallery}/{action=Index}/{id?}");
            });

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            new DBInitialiser(db);
        }
    }
}