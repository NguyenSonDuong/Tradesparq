using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IRespostory;
using Infrastructure.ImplimentRespostory;
using Application.IRespostory.IInfo;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var conn = config.GetConnectionString("DefaultConnection")!;

            // Pool cho hiệu năng cao (tùy dùng): AddDbContextPool<...>
            services.AddDbContextPool<AppDbContext>(opt =>
            {
                opt.UseMySql(conn, ServerVersion.AutoDetect(conn), my =>
                {
                    my.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
                    // my.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName); // nếu muốn gom migration
                });

                opt.EnableDetailedErrors();
                if (config.GetSection("EfCore:EnableSensitiveDataLogging").Exists() ? bool.Parse(string.IsNullOrEmpty(config.GetSection("EfCore:EnableSensitiveDataLogging").Value) ? config.GetSection("EfCore:EnableSensitiveDataLogging").Value : "false") : false)
                {
                    opt.EnableSensitiveDataLogging();
                }
            });
            services.Database.MigrateAsync()

            services.AddScoped<ICompanyRespostory, CompanyRespostory>();
            return services;
        }
    }
}
