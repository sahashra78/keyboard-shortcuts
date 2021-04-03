using Microsoft.EntityFrameworkCore;

namespace Keyboard_Shortcut_API.Models
{
    public class ShortcutContext : DbContext
    {
        public ShortcutContext(DbContextOptions<ShortcutContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperatingSystem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(c => c.Shortcuts)
                .WithOne(a => a.OperatingSystem)
                .HasForeignKey(a => a.OsId);
            });
            modelBuilder.Seed();
        }

        public DbSet<Shortcut> Shortcuts { get; set; }
        public DbSet<OperatingSystem> OperatingSystems { get; set; }

    }
}
