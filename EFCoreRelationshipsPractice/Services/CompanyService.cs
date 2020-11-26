using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Entities;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

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
            var companies = await companyDbContext.Companies
                .Include(company => company.ProfileEntity)
                .Include(company => company.Employees)
                .ToListAsync();
            return companies.Select(companyEntity => new CompanyDto(companyEntity)).ToList();
        }

        public async Task<CompanyDto> GetById(long id)
        {
            var foundCompanyEntity = await companyDbContext.Companies
                .FirstOrDefaultAsync(companyEntity => companyEntity.Id == id);
            return new CompanyDto(foundCompanyEntity);
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            var companyEntity = new CompanyEntity(companyDto);
            await companyDbContext.Companies.AddRangeAsync(companyEntity);
            await companyDbContext.SaveChangesAsync();
            return companyEntity.Id;
        }

        public async Task DeleteCompany(int id)
        {
            var foundCompany = await companyDbContext.Companies
                .FirstOrDefaultAsync(company => company.Id == id);
            companyDbContext.Companies.Remove(foundCompany);

            var foundProfile = await companyDbContext.Profiles
                .FirstOrDefaultAsync(profile => profile.Id == foundCompany.ProfileEntity.Id);
            companyDbContext.Profiles.Remove(foundProfile);

            foreach (var employeeEntity in foundCompany.Employees)
            {
                var foundEmployee = await companyDbContext.Employees.FirstOrDefaultAsync(employee => employee.Id == employeeEntity.Id);
                if (foundEmployee != null)
                {
                    companyDbContext.Employees.Remove(foundEmployee);
                }
            }

            await companyDbContext.SaveChangesAsync();
        }
    }
}