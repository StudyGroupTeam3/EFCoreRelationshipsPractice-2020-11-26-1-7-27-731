using EFCoreRelationshipsPractice.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsPractice.Repository
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {
        }

        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyEntity>(
                builder =>
                {
                    builder.HasMany<EmployeeEntity>(e => e.Employees).WithOne().HasForeignKey(e => e.CompanyEntityId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}