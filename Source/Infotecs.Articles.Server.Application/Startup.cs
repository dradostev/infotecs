namespace Infotecs.Articles.Server.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Autofac;
    using FluentValidation;
    using Infotecs.Articles.Server.Application.Services;
    using Infotecs.Articles.Server.Application.Validators;
    using Infotecs.Articles.Server.Database;
    using Infotecs.Articles.Server.Database.Repositories;
    using Infotecs.Articles.Server.Domain.Entities;
    using Infotecs.Articles.Server.Domain.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    /// <summary>
    /// ASP.NET Core startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Injects configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
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
            services.AddGrpc();
            services.AddGrpcReflection();
        }

        /// <summary>
        /// Configure Autofac DI container.
        /// </summary>
        /// <param name="builder">Injects Autofac container builder.</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder
                .Register(x =>
                    new ArticlesRepository(this.Configuration.GetConnectionString("Postgres")))
                .As<IArticlesRepository>();

            builder
                .Register(x =>
                    new CommentsRepository(this.Configuration.GetConnectionString("Postgres")))
                .As<ICommentsRepository>();

            builder
                .RegisterAssemblyTypes(GetType().Assembly)
                .Where(x => x.Name.EndsWith("Validator"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<AutofacValidatorFactory>()
                .As<IValidatorFactory>()
                .SingleInstance();
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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ArticlesService>();

                if (env.IsDevelopment())
                {
                    endpoints.MapGrpcReflectionService();
                }

                endpoints.MapGet(
                    "/",
                    async context =>
                    {
                        await context.Response.WriteAsync(
                            "Communication with gRPC endpoints must be made through a gRPC client.");
                    });
            });
        }
    }
}