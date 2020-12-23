using Infotecs.Articles.Client.Rpc.Services;
using Infotecs.Articles.Client.WebApp.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Infotecs.Articles.Client.WebApp
{
    /// <summary>
    /// ASP.NET Core startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration injection.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure Services.
        /// </summary>
        /// <param name="services">Injects Microsoft DI service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder.AddSerilog());
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            services.AddScoped<IArticlesRpcClient, ArticlesRpcClient>();
            services.AddSignalR();
            services.AddCors(options => 
            { 
                options.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader()); 
            });
            services.AddSingleton<ArticlesHub>();
        }

        /// <summary>
        /// Configure ASP.NET Core services.
        /// </summary>
        /// <param name="app">Injects application builder.</param>
        /// <param name="env">Injects hosting environment.</param>
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

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<ArticlesHub>("/events");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
