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

    string[] allowedOrigins = new[]
    {
        "http://localhost:5173",
        "http://app.example.com"
    };

    // Đăng ký CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("DefaultCors", policy =>
        {
            policy.WithOrigins(allowedOrigins)      // origin cụ thể
                  .AllowAnyHeader()                 // cho mọi header
                  .AllowAnyMethod()                 // GET/POST/PUT/DELETE...
                  .AllowCredentials()               // nếu cần cookie/bearer qua cross-site
                  .WithExposedHeaders("X-Total-Count")  // header client có thể đọc
                  .SetPreflightMaxAge(TimeSpan.FromHours(12)); // cache preflight
        });
    });

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
    app.UseCors("DefaultCors");
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