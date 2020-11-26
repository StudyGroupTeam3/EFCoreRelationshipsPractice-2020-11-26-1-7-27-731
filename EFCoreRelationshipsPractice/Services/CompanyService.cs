using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.NewFolder;
using EFCoreRelationshipsPractice.Repository;

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
            throw new NotImplementedException();
        }

        public async Task<CompanyDto> GetById(long id)
        {
            throw new NotImplementedException();
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