using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetLabWeb.Data;

[assembly: HostingStartup(typeof(PetLabWeb.Areas.Identity.IdentityHostingStartup))]
namespace PetLabWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentidadeContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetSection("Logging").GetSection("ConnectionStrings")["IdentidadeContextConnection"]));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<IdentidadeContext>();
            });
        }
    }
}