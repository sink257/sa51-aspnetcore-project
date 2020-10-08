using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Team8CA.Models;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Team8CA.DataAccess
{
    public class AppDbContext : DbContext
    {
        protected IConfiguration configuration;
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder model)
        //{
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Review>().HasOne<Products>(p => p.Products).WithMany(p => p.Reviews);


        }

        // neeed to implement the order and product methods in Models
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Products> Products { get; set; }
        //implementation of Model methods
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Session> Sessions { get; set; }



    }
}
