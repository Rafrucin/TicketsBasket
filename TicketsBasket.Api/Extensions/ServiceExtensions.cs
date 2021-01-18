using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Models.Data;
using TicketsBasket.Repositories;
using TicketsBasket.Services;
using Microsoft.Identity.Web;

namespace TicketsBasket.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentityOptions(this IServiceCollection services)
        {
            services.AddScoped<IdentityOptions>(sp =>
            {
                var context = sp.GetService<IHttpContextAccessor>().HttpContext;

                var identityOptions = new IdentityOptions();

                if (context.User.Identity.IsAuthenticated)
                {
                    string userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

                    identityOptions.UserId = userId;
                }

                return identityOptions;
            });
        }


        public static void AddB2CAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMicrosoftIdentityWebApiAuthentication( configuration, "AzureAdB2C");
        }

        public static void AddApplicationDatabaseContext (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("SqliteConnectionModels"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("TicketsBasket.Api");
                });
            });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
        }

        public static void AddUnitOfWork (this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EUnitOfWork>();
        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserProfilesService, UserProfilesServices>();
        }



    }
}
