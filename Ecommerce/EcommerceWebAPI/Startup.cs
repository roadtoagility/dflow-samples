using DFlow.Business.Cqrs;
using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Domain.Events;
using Ecommerce.Business.Extensions;
using Ecommerce.Persistence;
using Ecommerce.Persistence.ExtensionMethods;
using EcommerceWebAPI.ApiEndpoints;
using FluentMediator;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            services.AddSwaggerGen();

            services.AddDbContext<EcommerceAppDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("ModelConnection")));

            services.AddRepositories();
            services.AddCommandHandlers();
            services.AddMediatorOperations();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            
            app.UseRouting();

            app.UseEndpoints(EndpointRoutes.StateChangeApis);
        }
    }
}