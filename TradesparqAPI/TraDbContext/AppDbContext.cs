using Microsoft.EntityFrameworkCore;
using Tradesparq.Model.Company;
using Tradesparq.Model.Info;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace TradesparqAPI.TraDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Companies> Company => Set<Companies>();
        public DbSet<Cities> City => Set<Cities>();
        public DbSet<Emails> Email => Set<Emails>();
        public DbSet<Faxs> Fax => Set<Faxs>();
        public DbSet<PhoneNumbers> PhoneNumber => Set<PhoneNumbers>();
        public DbSet<PostalCodes> PostalCode => Set<PostalCodes>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Database defaults
            modelBuilder.HasCharSet("utf8mb4").UseCollation("utf8mb4_unicode_ci");

            // Company
            modelBuilder.Entity<Companies>(e =>
            {
                e.ToTable("Companies");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<Shipments>(e =>
            {
                e.ToTable("Shipments");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.HasIndex(x => x.IdShipments).IsUnique();

            });


            modelBuilder.Entity<Cities>(e =>
            {
                e.ToTable("Cities");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.City).IsRequired().HasMaxLength(64);
                e.Property(x => x.TypeInfo).IsRequired();
            });

            modelBuilder.Entity<Emails>(e =>
            {
                e.ToTable("Emails");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.Email).IsRequired().HasMaxLength(64);
                e.Property(x => x.TypeInfo).IsRequired();
            });

            modelBuilder.Entity<Faxs>(e =>
            {
                e.ToTable("Faxs");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.Fax).IsRequired().HasMaxLength(64);
                e.Property(x => x.TypeInfo).IsRequired();
            });

            modelBuilder.Entity<PhoneNumbers>(e =>
            {
                e.ToTable("PhoneNumbers");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.Phone).IsRequired().HasMaxLength(64);
                e.Property(x => x.TypeInfo).IsRequired();
            });

            modelBuilder.Entity<PostalCodes>(e =>
            {
                e.ToTable("PostalCodes");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.PostalCode).IsRequired().HasMaxLength(64);
                e.Property(x => x.TypeInfo).IsRequired();
            });

        }
    }
}
