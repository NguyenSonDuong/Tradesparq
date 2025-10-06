using Application.IJob;
using Application.IRespostory;
using Application.IRespostory.IAnalysis;
using Application.IRespostory.IAuthen;
using Application.IRespostory.ICommand;
using Application.IRespostory.IInfo;
using Application.IService;
using Application.Respostory;
using CrawlService.Controller;
using DabacoControl.api;
using Infrastructure.DBContext;
using Infrastructure.ImplimentJob;
using Infrastructure.ImplimentRespostory;
using Infrastructure.ImplimentRespostory.Authen;
using Infrastructure.ImplimentRespostory.CommandR;
using Infrastructure.ImplimentRespostory.Info;
using Infrastructure.ImplimentService;
using Infrastructure.ImplimentService.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            
            services.AddDbContextPool<AppDbContext>(opt =>
            {
                var conn = config.GetConnectionString("DefaultConnection")!;
                var migAsm = typeof(AppDbContext).Assembly.GetName().Name; // "Infrastructure"
                opt.UseMySql(conn, ServerVersion.AutoDetect(conn),
                    my => my.MigrationsAssembly(migAsm));   // SQL Server: UseSqlServer(conn, b => b.MigrationsAssembly(migAsm))
                opt.EnableSensitiveDataLogging(false);
            });
            //services.AddSingleton(typeof(AppDbContext));
            services.AddScoped<IAuthenTradesparqRespostory, AuthenTradesparqRespostory>();

            services.AddScoped<IEmailRespostory, EmailRespostory>();
            services.AddScoped<IPhoneNumberRespostory, PhoneNumberRespostory>();
            services.AddScoped<IFaxRespostory, FaxRespostory>();
            services.AddScoped<IPostalCodeRespostory, PostalCodeRespostory>();
            services.AddScoped<ICityRespostory, CityRespostory>();
            
            services.AddScoped<IApiBaseController, ApiBaseController>();

            services.AddScoped<IRequestService, RequestService>();

            services.AddScoped<IRequestSearchHistoryRespostory, RequestSearchHistoryRespostory>();
            services.AddScoped<IShippentRespostory, ShippentRespostory>();
            services.AddScoped<ICompanyRespostory, CompanyRespostory>();
            services.AddScoped<ICommandRespostory, CommandRespostory>();

            services.AddScoped<IShippentService, ShippentService>();
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddScoped<IAutoCrawlJob, AutoCrawlJob>();

            // Đăng ký job thực thi
            return services;
        }
    }
}
