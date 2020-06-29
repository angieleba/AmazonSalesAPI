

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
            this.options = options;
        }

        public SalesContext()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserProducts> UserProducts { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mapping one-Many User with Product
            modelBuilder.Entity<User>(e =>
            {
                e.HasMany(p => p.Products)
                .WithOne(a => a.User)
                .HasForeignKey(f => f.UserId);
            });

            //Mapping many to many user with products
            modelBuilder.Entity<UserProducts>().HasKey(up => new { up.UserId, up.ProductId });
            modelBuilder.Entity<UserProducts>(e =>
            {
                e.HasOne(p => p.User)          
                .WithMany(a => a.BookmarkedProducts)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<UserProducts>(e =>
            {
                e.HasOne(p => p.Product)
               .WithMany(a => a.BookmarkingUsers)
               .HasForeignKey(a => a.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

            });

            //Mapping many-many of user and user for following
            modelBuilder.Entity<UserFollowing>().HasKey(k => new { k.UserId, k.FollowedUserId });
            modelBuilder.Entity<UserFollowing>(e =>
            {
               e.HasOne(p => p.User)
               .WithMany(a => a.Following)
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserFollowing>(e =>
            {
                e.HasOne(p => p.FollowedUser)
                .WithMany(a => a.Followers)
                .HasForeignKey(a => a.FollowedUserId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
