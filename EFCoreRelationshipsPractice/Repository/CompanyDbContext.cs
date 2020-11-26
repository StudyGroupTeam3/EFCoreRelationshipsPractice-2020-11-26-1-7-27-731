using EFCoreRelationshipsPractice.Dtos;
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

        public DbSet<CompanyEntity> Companies { get; set; } // 在Db中创建了一个Companies的表格
    }
}