using Microsoft.EntityFrameworkCore;
using NameDraw.Api.Entities;

namespace NameDraw.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<NameItem> Names => Set<NameItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NameItem>(e =>
            {
                e.ToTable("Names");
                e.HasKey(x => x.Id);
                e.Property(x => x.Value)
                    .IsRequired()
                    .HasMaxLength(50);
                e.Property(x => x.CreatedAt)
                    .IsRequired();
            });
        }
    }
}
