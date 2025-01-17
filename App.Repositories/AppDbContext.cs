using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace App.Repositories
{
    public class AppDbContext : DbContext
    {
        private readonly DbContextOptions<AppDbContext> _options;

        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _options = options;
        }

        
        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }

    // Design-time DbContext factory
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // Bağlantı dizesi ile Npgsql ayarları
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=NetNlayerAndCleanArchitectureDb;Username=postgres;Password=toranjj;");
            
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}