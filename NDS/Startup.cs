using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NDS.Models.Context;
using NDS.Models.Domain;
using NDS.Models.Repository;
using NDS.Models.Services;
using NDS.Models.UnitOfWork;
using NDS.Utility;
using System;

namespace NDS
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {

            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
           .AddEnvironmentVariables();

            Configuration = builder.Build();




        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //verify Connection String 
            services.AddDbContext<NDSDbContext>(option =>
                 option.UseSqlServer(Configuration.GetConnectionString("MyConnectionString"))
            );



            //verify Idenity service
            //services.AddIdentity<ApplicationUsers, ApplicationRoles>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();


            //services.ConfigureApplicationCookie(option => option.LoginPath = "/Admin/Account/Login");



            services.AddMvc();
            services.AddControllersWithViews();
            services.AddRazorPages();



            #region IOC
            //services.AddScoped<IAspNetUserRoleRepository, AspNetUserRoleRepository>();
            services.AddScoped<ILogger, Logger>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUploadFile, UploadFile>();
            services.AddScoped<IDeleteFile, DeleteFile>();
            #endregion



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/LogOut";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
            });





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
            }


            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {


                endpoints.MapAreaControllerRoute(
                     name: "Admin",
                     areaName: "Admin",
                     pattern: "Admin/{controller=Home}/{action=Index}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}");



                endpoints.MapRazorPages();



                endpoints.MapRazorPages();

            });

        }




    }
}
