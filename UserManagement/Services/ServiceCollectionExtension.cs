using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Areas.Identity;
using UserManagement.Data;

namespace UserManagement.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UserDbContext>(o => o.UseMySQL(connectionString), ServiceLifetime.Scoped);
            services.AddScoped<UserService>();

            return services;
        }
    }
}
