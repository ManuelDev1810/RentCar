using CarService.Core.Interfaces;
using CarService.Infrastructure;
using CarService.Infrastructure.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace CarService
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarService", Version = "v1" });
            });

            //MassTransit
            services.AddMassTransit(config => {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration["Queues:RabbitMQ:DefaultHost:Host"]);
                });
            });

            services.AddMassTransitHostedService();

            //Dependecy Injections
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<ITypeOfFuelRepository, TypeOfFuelRepository>();
            services.AddTransient<ITypeOfCarRepository, TypeOfCarRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IModelRepository, ModelRepository>();

            //Adding the Dependecy Injections for the Car Infrastructue
            services.AddInfrastructure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarService v1"));
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
