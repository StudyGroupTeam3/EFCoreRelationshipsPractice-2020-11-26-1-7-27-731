﻿using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Entities;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .Include(company => company.ProfileEntity)
                .Include(company => company.Employees)
                .FirstAsync(companyEntity => companyEntity.Id == id);
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
                .Include(company => company.ProfileEntity)
                .Include(company => company.Employees)
                .FirstOrDefaultAsync(company => company.Id == id);

            companyDbContext.Companies.Remove(foundCompany);

            await companyDbContext.SaveChangesAsync();
        }
    }
}