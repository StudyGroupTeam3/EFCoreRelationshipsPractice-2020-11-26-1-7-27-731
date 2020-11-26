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

        public CompanyService(CompanyDbContext companyDbContext) //DbContext was created in the StartUp
        {
            this.companyDbContext = companyDbContext;
        }

        public async Task<List<CompanyDto>> GetAll()
        {
            var companies = await this.companyDbContext.Companies.ToListAsync();
            return companies.Select(companyEntity => new CompanyDto(companyEntity)).ToList();
        }

        public async Task<CompanyDto> GetById(int id)
        {
            var company = await this.companyDbContext.Companies.FirstOrDefaultAsync(companyEntity => companyEntity.ID == id);
            return new CompanyDto(company);
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            CompanyEntity company = new CompanyEntity()
            {
                Name = companyDto.Name
            };

            await this.companyDbContext.Companies.AddAsync(company);
            await this.companyDbContext.SaveChangesAsync();
            return company.ID;
        }

        public async Task DeleteCompany(int id)
        {
            throw new NotImplementedException();
        }
    }
}