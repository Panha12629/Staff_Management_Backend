using Microsoft.EntityFrameworkCore;
using Staff_Management.Application.Common.Interfaces;
using Staff_Management.Domain.Entities;

namespace Staff_Management.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StaffId).HasMaxLength(8);
                entity.Property(e => e.FullName).HasMaxLength(100);
                entity.Property(e => e.Gender).IsRequired();
                entity.Property(e => e.Birthday)
                      .IsRequired()
                      .HasColumnType("timestamptz"); 
            });
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync(CancellationToken.None);
        }
    }
}
