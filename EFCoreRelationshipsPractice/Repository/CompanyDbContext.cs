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
            modelBuilder.Entity<CompanyEntity>()
                .HasOne(a => a.ProfileEntity)
                .WithOne(b => b.Company)
                .HasForeignKey<ProfileEntity>(b => b.CompanyId);
        }
    }
}