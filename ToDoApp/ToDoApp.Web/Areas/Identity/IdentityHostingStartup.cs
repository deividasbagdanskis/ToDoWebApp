using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;

[assembly: HostingStartup(typeof(ToDoApp.Web.Areas.Identity.IdentityHostingStartup))]
namespace ToDoApp.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SampleWebAppContext>(options =>
                    options.UseSqlServer(context.Configuration.GetConnectionString("SampleWebAppContext")));

                services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SampleWebAppContext>();
            });
        }
    }
}