

using AmazonSales.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Data;

namespace AmazonSales.Data.Db
{
    public class SalesContext : DbContext
    {
        private DbContextOptions<SalesContext> options;

        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
            this.options = options;
        }

        public SalesContext()
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
