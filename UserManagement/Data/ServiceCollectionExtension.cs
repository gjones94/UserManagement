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

namespace UserManagement.Data
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterUserServices(this IServiceCollection services, string connectionString) 
        {
            services.AddDbContext<UserDbContext>(o => o.UseMySQL(connectionString), ServiceLifetime.Scoped);

            return services;
        }
    }
}
