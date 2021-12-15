using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Infrastructure.Models.Identity;
using Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Services;
using Services.Interfaces;
using AutoMapper;
using Infrastructure.MappingProfile;
using System.IO;

namespace MotoGarage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region register options
            var mongoDbSettings = Configuration.GetSection(nameof(MongoDbOption));
            services.Configure<MongoDbOption>(mongoDbSettings);
            var emailSMTPSettings = Configuration.GetSection(nameof(EmailSMTPOption));
            services.Configure<EmailSMTPOption>(emailSMTPSettings);
            #endregion

            var mongoOption = mongoDbSettings.Get<MongoDbOption>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                            .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
                                mongoOption.ConnectionString, mongoOption.AppName)
                            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            services.Configure<IdentityOptions>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".MotoGarage.Application";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IAccountManagerService, AccountManagerService>();
            services.AddScoped<IAccountAuthService, AccountAuthService>();
            services.AddScoped<IServiceRequestService, ServiceRequestService>();

            services.AddSingleton<INavMenuService, NavMenuService>();
            services.AddSingleton<IEmailService, EmailService>();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}/{id?}");
            });

            app.UseSpaStaticFiles(new StaticFileOptions
            {
                RequestPath = "/ClientApp/build"
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Join(env.ContentRootPath, "ClientApp"); ;

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
