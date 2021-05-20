using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApniShop.Areas.Identity.Data;
using ApniShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApniShop.Data
{
    public class ApniShopContext : IdentityDbContext<ApniShopUser>
    {
        public ApniShopContext(DbContextOptions<ApniShopContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Wants_ProductApniShopUser>()
                .HasKey(wpa => new { wpa.ApniShopUserID, wpa.ProductID });
            builder.Entity<Wants_ProductApniShopUser>()
                .HasOne<Product>(wpa => wpa.Product)
                .WithMany(a => a.Wanters)
                .HasForeignKey(wpa => wpa.ProductID);
            builder.Entity<Wants_ProductApniShopUser>()
                .HasOne<ApniShopUser>(wpa => wpa.ApniShopUser)
                .WithMany(p => p.Wants)
                .HasForeignKey(wpa => wpa.ApniShopUserID);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Wants_ProductApniShopUser> Wants_ProductApniShopUser { get; set; }
    }
}
