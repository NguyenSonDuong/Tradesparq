using AutoMapper;
using CrawlService.Dto;
using CrawlService.Dto.Request;
using CrawlService.Dto.Responsive;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using TestRequest.Dto.Request;
using Tradesparq.Controller;
using Tradesparq.Dto.ResponsiveDto;
using Tradesparq.ProfileMapper;
using Tradesparq.Respostory;
using Tradesparq.Respostory.Abtrac;
using Tradesparq.Service;
using AutoMapper;
using Infrastructure;
var builder = Host.CreateApplicationBuilder(args);


// 1) Config
builder.Configuration
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddEnvironmentVariables();

var logger = LogManager.Setup().LoadConfigurationFromFile("NLog.config", optional: true)
                               .GetCurrentClassLogger();
logger.Info("Setting Database");
try
{

    
    builder.Logging.ClearProviders();
    builder.Logging.AddNLog();

    // 3) EF Core DbContext
    var cs = builder.Configuration.GetConnectionString("Default")
             ?? throw new InvalidOperationException("Missing ConnectionStrings:Default.");
    logger.Info("Connect database");
    builder.Services.AddDbContext<AppDbContext>(opt =>
    {
        opt.UseMySql(
            cs,
            ServerVersion.AutoDetect(cs),
            my => my.EnableRetryOnFailure()
                    .MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
        );
        opt.EnableSensitiveDataLogging(false);
        opt.EnableDetailedErrors(true);
    });
    builder.Services.AddAutoMapper(typeof(AppMappingProfile));
    builder.Services.AddInfrastructure(builder.Configuration);
    // 4) Build host
    using var host = builder.Build();

    // 5) (Dev) Apply migration khi chạy app
    var scope = host.Services.CreateScope();
    db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();


    logger.Info("Done config Database");

}
catch (Exception ex)
{
    logger.Error(ex,"Error");
}

