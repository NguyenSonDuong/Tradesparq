using Api.Job;
using Application.IJob;
using Application.Mapping;
using Infrastructure;
using Infrastructure.DBContext;
using Infrastructure.ImplimentJob;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
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
    builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", Microsoft.Extensions.Logging.LogLevel.None);
    builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", Microsoft.Extensions.Logging.LogLevel.Warning);
    builder.Host.UseNLog();

    builder.Services.AddAutoMapper(typeof(RequestMapper));
    // Đăng ký các dịch vụ từ Infrastructure
    builder.Services.AddInfrastructure(builder.Configuration);

    // Cấu hình option cho job
    builder.Services.Configure<AutoCrawlJobOption>(builder.Configuration.GetSection("Jobs:MyJob"));
    builder.Services.AddSingleton(res => res.GetRequiredService<Microsoft.Extensions.Options.IOptions<AutoCrawlJobOption>>().Value);
    //var cs = builder.Configuration.GetConnectionString("DefaultConnection")
    //         ?? throw new InvalidOperationException("Missing ConnectionStrings:Default.");
    //builder.Services.AddDbContext<AppDbContext>(opt =>
    //{
    //    opt.UseMySql(
    //        cs,
    //        ServerVersion.AutoDetect(cs),
    //        my => my.EnableRetryOnFailure()
    //                .MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
    //    );
    //    opt.EnableSensitiveDataLogging(false);
    //    opt.EnableDetailedErrors(true);
    //});
    // Đăng ký hosted service chạy nền
    builder.Services.AddHostedService<AutoCrawlJobService>();
    builder.Logging.AddSimpleConsole(o =>
    {
        o.ColorBehavior = LoggerColorBehavior.Enabled; // ép bật màu
        o.SingleLine = false;
    });
    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    using (var scope = app.Services.CreateScope())
    {
        var cfg = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var auto = cfg.GetValue("EfCore:AutoMigrate", defaultValue: false);
        static bool IsEfDesignTime()
        => AppDomain.CurrentDomain.GetAssemblies()
       .Any(a => a.GetName().Name == "Microsoft.EntityFrameworkCore.Design");
        if (!IsEfDesignTime())
        {
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