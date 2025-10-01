using Domain.Entities;
using Domain.Entities.Authen;
using Domain.Entities.command;
using Domain.Entities.EntityAnalysis;
using Domain.Entities.InfoCompany;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Email> Emails => Set<Email>();
        public DbSet<Fax> Faxs => Set<Fax>();
        public DbSet<PhoneNumber> PhoneNumbers => Set<PhoneNumber>();
        public DbSet<PostalCode> PostalCodes => Set<PostalCode>();
        public DbSet<Shipment> Shipments => Set<Shipment>();
        public DbSet<RequestSearchHisory> RequestSearchHisories => Set<RequestSearchHisory>();
        public DbSet<AuthenTradesparq> AuthenTradesparqs => Set<AuthenTradesparq>();
        public DbSet<Command> Commands => Set<Command>();

        

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4").UseCollation("utf8mb4_unicode_ci");

            // Company
            modelBuilder.Entity<RequestSearchHisory>(e =>
            {
                e.ToTable("RequestSearchHisory");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();

            });
            // Company
            modelBuilder.Entity<AuthenTradesparq>(e =>
            {
                e.ToTable("AuthenTradesparq");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();

            });
            // Company
            modelBuilder.Entity<Company>(e =>
            {
                e.ToTable("Company");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x=>x.Uuid).IsRequired().HasMaxLength(64);
                e.HasIndex(x => x.Uuid).IsUnique();

            });
            // Command
            modelBuilder.Entity<Command>(e =>
            {
                e.ToTable("Command");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.SearchKey).IsRequired().HasMaxLength(50);
                e.Property(x => x.TypeSearch).IsRequired().HasMaxLength(50);

            });
            modelBuilder.Entity<Shipment>(e =>
            {
                e.ToTable("Shipment");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.HasIndex(x => x.IdShipments).IsUnique();

            });


            modelBuilder.Entity<City>(e =>
            {
                e.ToTable("City");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.CityName).HasColumnName("City").IsRequired().HasMaxLength(64);
                e.Property(x => x.TypeInfo).IsRequired();
            });

            modelBuilder.Entity<Email>(e =>
            {
                e.ToTable("Email");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.EmailAddress).HasColumnName("Email").IsRequired().HasMaxLength(64);
                e.Property(x => x.TypeInfo).IsRequired();
            });

            modelBuilder.Entity<Fax>(e =>
            {
                e.ToTable("Fax");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.FaxNumber).HasColumnName("Fax").IsRequired().HasMaxLength(64);
                e.Property(x => x.TypeInfo).IsRequired();
            });

            modelBuilder.Entity<PhoneNumber>(e =>
            {
                e.ToTable("PhoneNumber");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.PhoneNum).HasColumnName("Phone").IsRequired().HasMaxLength(64);
                e.Property(x => x.TypeInfo).IsRequired();
            });

            modelBuilder.Entity<PostalCode>(e =>
            {
                e.ToTable("PostalCode");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.PostalCodeNumber).HasColumnName("PostalCode").IsRequired().HasMaxLength(64);
                e.Property(x => x.TypeInfo).IsRequired();
            });
        }
    }
}
