using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace App.Repositories
{
    // DbContext sınıfını DbContext'ten türetmelisiniz
    public class AppDbContext : DbContext
    {
        // DbContextOptions parametresi alarak constructor'ı tanımlayın
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet'ler burada tanımlanır
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