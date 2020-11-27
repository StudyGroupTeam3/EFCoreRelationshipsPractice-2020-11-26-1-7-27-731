using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Entities;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsPractice.Services
{
    public class CompanyService
    {
        private readonly CompanyDbContext companyDbContext;

        public CompanyService(CompanyDbContext companyDbContext)
        {
            this.companyDbContext = companyDbContext;
        }

        public async Task<List<CompanyDto>> GetAll()
        {
            var companies = await companyDbContext.Companies.Include(company => company.Profile).Include(company => company.Employees).ToListAsync();
            return companies.Select(compantEntity => new CompanyDto(compantEntity)).ToList();
        }

        public async Task<CompanyDto> GetById(int id)
        {
            var companyEntity = await companyDbContext.Companies.FirstOrDefaultAsync(companyEntity => companyEntity.Id == id);
            return new CompanyDto(companyEntity);
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            CompanyEntity companyEntity = new CompanyEntity(companyDto);
            await companyDbContext.Companies.AddAsync(companyEntity);
            await companyDbContext.SaveChangesAsync();
            return companyEntity.Id;
        }

        public async Task DeleteCompany(int id)
        {
            var company = await companyDbContext.Companies.Include(c => c.Profile).Include(c => c.Employees).FirstOrDefaultAsync(companyEntity => companyEntity.Id == id);
            companyDbContext.Companies.Remove(company);
            await companyDbContext.SaveChangesAsync();
        }
    }
}