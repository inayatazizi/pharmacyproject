using System;
using LabManagmentSystem.Data;
using LabManagmentSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(LabManagmentSystem.Areas.Identity.IdentityHostingStartup))]
namespace LabManagmentSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<PathoDbContext>(options =>
            //    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

            //    services.AddDefaultIdentity<PathoUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<PathoDbContext>();
            });
        }
    }
}