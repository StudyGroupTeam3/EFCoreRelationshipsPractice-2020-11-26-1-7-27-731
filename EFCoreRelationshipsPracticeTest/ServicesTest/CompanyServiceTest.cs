using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace EFCoreRelationshipsPracticeTest.ServicesTest
{
    [Collection("UsingInMemoryDB")]
    public class CompanyServiceTest : TestBase
    {
        public CompanyServiceTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_company_using_CompanyService_AddCompany_success()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var companyContext = scopedServices.GetRequiredService<CompanyDbContext>();
            companyContext.Database.EnsureDeleted();
            companyContext.Database.EnsureCreated();

            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>() { new EmployeeDto() { Name = "Tom", Age = 19 } },
                Profile = new ProfileDto() { RegisteredCapital = 100010, CertId = "100" }
            };

            var companyService = new CompanyService(companyContext);
            await companyService.AddCompany(companyDto);

            Assert.Equal(1, companyContext.Companies.Count());
        }

        [Fact]
        public async Task Should_get_a_company_by_id_using_CompanyService_GetById_success()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var companyContext = scopedServices.GetRequiredService<CompanyDbContext>();
            companyContext.Database.EnsureDeleted();
            companyContext.Database.EnsureCreated();

            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>() { new EmployeeDto() { Name = "Tom", Age = 19 } },
                Profile = new ProfileDto() { RegisteredCapital = 100010, CertId = "100" }
            };

            var companyService = new CompanyService(companyContext);
            await companyService.AddCompany(companyDto);

            var actualCompanyEntity = companyService.GetById(1);

            Assert.Equal(1, actualCompanyEntity.Id);
        }

        [Fact]
        public async Task Should_delete_a_company_by_id_using_CompanyService_DeleteCompany_success()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var companyContext = scopedServices.GetRequiredService<CompanyDbContext>();
            companyContext.Database.EnsureDeleted();
            companyContext.Database.EnsureCreated();

            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>() { new EmployeeDto() { Name = "Tom", Age = 19 } },
                Profile = new ProfileDto() { RegisteredCapital = 100010, CertId = "100" }
            };

            var companyService = new CompanyService(companyContext);
            await companyService.AddCompany(companyDto);

            await companyService.DeleteCompany(1);

            var i = companyContext.Employees.Count();
            var j = companyContext.Profiles.Count();

            Assert.Equal(0, companyContext.Companies.Count());
        }
    }
}
