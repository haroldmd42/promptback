using Microsoft.EntityFrameworkCore;
using VisageAI.Api.Models;

namespace VisageAI.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Prompt> Prompts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Prompt>()
                .Property(p => p.Image)
                .HasColumnType("longtext");
        }
    }
}
