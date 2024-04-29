using Microsoft.EntityFrameworkCore;
using SSS_StoreStockSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSS_StoreStockSystem.DAL.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        { 

        }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<StoreItem> StoreItems { get; set; }
        public DbSet<StockLog> StockLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StoreItem>()
                .HasOne(si => si.Store)
                .WithMany(s => s.StoreItems)
                .HasForeignKey(si => si.StoreId);

            modelBuilder.Entity<StoreItem>()
                .HasOne(si => si.Item)
                .WithMany(i => i.StoreItems)
                .HasForeignKey(si => si.ItemId);

            modelBuilder.Entity<StoreItem>()
                .HasKey(si => new { si.StoreId, si.ItemId });
            modelBuilder.Entity<StockLog>()
                .HasKey(l => new { l.StoreId, l.ItemId, l.DateTime });
            modelBuilder
                .Entity<StockLog>()
                .Property(l => l.Process)
                .HasConversion<string>();
        }

    }
}
