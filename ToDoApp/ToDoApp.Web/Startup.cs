using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoApp.Data.Context;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services;
using ToDoApp.Business.Services.InDbProviders;
using ToDoApp.Business.Services.InFileProviders;
using ToDoApp.Business.Services.InMemoryProviders;
using System;
using ToDoApp.Projects.ApiClient;

namespace ToDoApp
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllersWithViews();
            services.AddRazorPages();

            switch (Configuration.GetValue<string>("ProviderType"))
            {
                case "InMemory":
                    services.AddSingleton<IDataProvider<ToDoItemVo>, InMemoryToDoItemProvider>();
                    services.AddSingleton<IDataProvider<CategoryVo>, InMemoryCategoryProvider>();
                    break;
                case "InFile":
                    services.AddSingleton<IDataProvider<ToDoItemVo>, InFileToDoItemProvider>();
                    services.AddSingleton<IDataProvider<CategoryVo>, InFileCategoryProvider>();
                    break;
                case "InDatabase":
                    services.AddScoped<IAsyncDbDataProvider<CategoryVo>, InDbCategoryProvider>();
                    services.AddScoped<IAsyncDbDataProvider<ToDoItemVo>, InDbToDoItemProvider>();
                    services.AddScoped<IAsyncDbDataProvider<TagVo>, InDbTagProvider>();
                    services.AddScoped<IInDbToDoItemTagProvider, InDbToDoItemTagProvider>();
                    services.AddScoped<IInDbProjectToDoItemProvider, InDbToDoItemProvider>();
                    break;
                default:
                    break;
            }

            services.AddSingleton<IApiClient>(new ApiClient("https://localhost:44343"));

            services.AddDbContext<SampleWebAppContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SampleWebAppContext")));
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
