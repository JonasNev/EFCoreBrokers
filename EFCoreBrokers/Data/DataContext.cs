using EFCoreBrokers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBrokers.Data
{
    public class DataContext : DbContext
    {
        public DbSet<BrokerModel> Brokers { get; set; }
        public DbSet<ApartmentModel> Apartments { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompaniesBrokers>()
                .HasKey(cb => new { cb.BrokerId, cb.CompanyId });
            modelBuilder.Entity<CompaniesBrokers>()
                .HasOne(bc => bc.Broker)
                .WithMany(b => b.CompaniesBrokers)
                .HasForeignKey(bc => bc.BrokerId);
            modelBuilder.Entity<CompaniesBrokers>()
                .HasOne(bc => bc.Company)
                .WithMany(c => c.CompaniesBrokers)
                .HasForeignKey(bc => bc.CompanyId);

        }
    }
}
