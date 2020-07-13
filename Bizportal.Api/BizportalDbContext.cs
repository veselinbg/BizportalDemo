using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bizportal.Api
{
    public class BizportalDbContext : DbContext
    {
        private readonly string _connectionString;
        public BizportalDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BizportalDemoDb");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                        .HasMany(p => p.Products)
                        .WithOne(c => c.Category)
                        .IsRequired();

            modelBuilder.Entity<Order>()
                        .HasOne(c => c.Client)
                        .WithMany(o => o.Orders)
                        .HasForeignKey(x => x.ClientId)
                        .IsRequired();

            modelBuilder.Entity<Order>()
                        .HasOne(p => p.Product)
                        .WithMany(o => o.Orders)
                        .HasForeignKey(x => x.ProductId)
                        .IsRequired();

            modelBuilder.Entity<Wallet>()
                        .HasOne(x => x.Client)
                        .WithOne(w => w.Wallet)
                        .HasForeignKey<Client>(c => c.WalletId)
                        .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
