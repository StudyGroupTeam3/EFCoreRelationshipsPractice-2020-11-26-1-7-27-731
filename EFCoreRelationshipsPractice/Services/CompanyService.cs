using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.NewFolder;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsPractice.Services
{
    public class CompanyService
    {
        private readonly CompanyDbContext companyDbContext;

        public CompanyService(CompanyDbContext companyDbContext) //DbContext was created in the StartUp， This service should get all DB method-
        {
            this.companyDbContext = companyDbContext;
        }

        public async Task<List<CompanyDto>> GetAll()
        {
            var companies = await this.companyDbContext.Companies.
                Include(company => company.Profile).
                Include(company => company.Employees).ToListAsync();
            return companies.Select(companyEntity => new CompanyDto(companyEntity)).ToList();
        }

        public async Task<CompanyDto> GetById(int id)
        {
            var company = await this.companyDbContext.Companies
                .Include(company => company.Profile).
                Include(company => company.Employees)
                .FirstOrDefaultAsync(companyEntity => companyEntity.ID == id);
            return new CompanyDto(company);
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            CompanyEntity company = new CompanyEntity(companyDto)
            {
            };

            await this.companyDbContext.Companies.AddAsync(company);
            await this.companyDbContext.SaveChangesAsync();
            return company.ID;
        }

        public async Task DeleteCompany(int id)
        {
            var foundCompany = await this.companyDbContext.Companies.FirstOrDefaultAsync(company => company.ID == id);
            this.companyDbContext.Companies.Remove(foundCompany);
            await this.companyDbContext.SaveChangesAsync();
        }
    }
}