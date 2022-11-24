using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VareskanningModels.SQL;
#nullable disable

namespace VareskanningModels.DB
{
    public class ItemScannerDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public string ConnectionString { get; } = @"Server=(localdb)\mssqllocaldb;Database=ItemScannerDb;Trusted_Connection=True;";
        public ItemScannerDbContext() : base()
        {
        }

        public ItemScannerDbContext(string connectionString) 
        {
            ConnectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
            //optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
