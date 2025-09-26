using Api.Job;
using Application.IJob;
using Application.Mapping;
using Infrastructure;
using Infrastructure.DBContext;
using Infrastructure.ImplimentJob;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    builder.Services.AddAutoMapper(typeof(RequestMapper).Assembly);
    builder.Services.AddInfrastructure(builder.Configuration);
    // Cấu hình option cho job
    builder.Services.Configure<AutoCrawlJobOption>(builder.Configuration.GetSection("Jobs:MyJob"));

    // Đăng ký job thực thi
    builder.Services.AddScoped<IAutoCrawlJob, AutoCrawlJob>();

    // Đăng ký hosted service chạy nền
    builder.Services.AddHostedService<AutoCrawlJobService>();
    using (var scope = app.Services.CreateScope())
    {
        var cfg = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var auto = cfg.GetValue("EfCore:AutoMigrate", defaultValue: false);

        if (auto || app.Environment.IsDevelopment())
        {
            try
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                logger.Info("Applying EF Core migrations...");
                await db.Database.MigrateAsync();
                logger.Info("EF Core migrations applied.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "EF Core migration failed at startup.");
                throw; // tuỳ bạn: dừng app nếu migrate fail
            }
        }
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}