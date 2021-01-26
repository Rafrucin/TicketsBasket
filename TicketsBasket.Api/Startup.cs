using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsBasket.Models.Data;
using TicketsBasket.Api.Extensions;
using TicketsBasket.Api.Options;

namespace TicketsBasket.Api
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
            services.ConfigureCors();
            
            services.AddB2CAuthentication(Configuration);

            services.AddBusinessServices();

            services.AddApplicationDatabaseContext(Configuration);

            services.AddHttpContextAccessor();

            services.ConfigureIdentityOptions();

            services.AddControllers();

            services.AddUnitOfWork();

            services.AddSwaggerGen();

            services.AddAzureStorageOptions(Configuration);

            services.AddInfrastuctureServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseSwagger();

            app.UseSwagger();

            app.UseSwaggerUI( swagger=>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketsBasket API V1.0");
            }
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
