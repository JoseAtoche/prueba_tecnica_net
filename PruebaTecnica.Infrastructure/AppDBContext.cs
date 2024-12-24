using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<BankEntity> Banks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankEntity>(entity =>
        {
            entity.HasKey(b => b.BIC);

            entity.HasIndex(b => b.BIC).IsUnique();

            entity.Property(b => b.BIC)
                .HasMaxLength(8)
                .IsRequired();

            entity.Property(b => b.Name)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(b => b.Country)
                .HasMaxLength(2)
                .IsRequired();
        });
    }
}
