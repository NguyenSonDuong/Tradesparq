using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace TradesparqAPI.TraDbContext
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var cs = config.GetConnectionString("MyDb")!;
            var builder = new DbContextOptionsBuilder<AppDbContext>();

            builder.UseMySql(cs, ServerVersion.AutoDetect(cs),
                o => o.SchemaBehavior(MySqlSchemaBehavior.Ignore));

            return new AppDbContext(builder.Options);
        }
    }
}
