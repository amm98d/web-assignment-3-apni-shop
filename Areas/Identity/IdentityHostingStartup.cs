using System;
using ApniShop.Areas.Identity.Data;
using ApniShop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ApniShop.Areas.Identity.IdentityHostingStartup))]
namespace ApniShop.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApniShopContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ApniShopContextConnection")));

                services.AddDefaultIdentity<ApniShopUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApniShopContext>();
            });
        }
    }
}