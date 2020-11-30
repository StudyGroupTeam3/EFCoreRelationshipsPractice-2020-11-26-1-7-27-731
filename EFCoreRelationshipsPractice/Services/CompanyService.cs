using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Entities;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsPractice.Services
{
    public class CompanyService
    {
        private readonly CompanyDbContext companyDbContext;

        public CompanyService(CompanyDbContext companyDbContext) // IOC容器新建了一个对象通过构造方法注入进来
        {
            this.companyDbContext = companyDbContext;
        }

        public async Task<List<CompanyDto>> GetAll()
        {
            var companies = await this.companyDbContext.Companies
                .Include(company => company.Profile)
                .Include(company => company.Employees)
                .ToListAsync();
            return companies.Select(companyEntity => new CompanyDto(companyEntity)).ToList();
        }

        public async Task<CompanyDto> GetById(int id)
        {
            var foundCompany = await this.companyDbContext.Companies.FirstOrDefaultAsync(companyEntity => companyEntity.Id == id);
            return new CompanyDto(foundCompany);
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            CompanyEntity companyEntity = new CompanyEntity(companyDto);
            await this.companyDbContext.Companies.AddAsync(companyEntity);
            await this.companyDbContext.SaveChangesAsync();
            return companyEntity.Id;
        }

        public async Task DeleteCompany(int id)
        {
            var foundCompany = await this.companyDbContext.Companies.FirstOrDefaultAsync(company => company.Id == id);
            this.companyDbContext.Companies.Remove(foundCompany);
            await this.companyDbContext.SaveChangesAsync();
        }
    }
}