using EFCoreRelationshipsPractice;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EFCoreRelationshipsPracticeTest.ControllerTest
{
    [Collection("TestClass1")]
    public class CompanyServiceTest : TestBase
    {
        private CompanyDto companyDto = new CompanyDto();
        private CompanyDbContext context;
        private CompanyService companyService;
        public CompanyServiceTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
            companyDto = new CompanyDto
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>()
                {
                    new EmployeeDto() { Name = "Tom", Age = 19 },
                    new EmployeeDto() { Name = "Tom2", Age = 20 },
                },
                Profile = new ProfileDto() { RegisteredCapital = 100010, CertId = "100", },
            };

            var scope = Factory.Services.CreateScope();
            var scopeService = scope.ServiceProvider;
            context = scopeService.GetRequiredService<CompanyDbContext>();
            companyService = new CompanyService(context);
        }

        [Fact]
        public async Task Should_create_company_employee_profile_success_via_company_service()
        {
            // given

            // when
            await companyService.AddCompany(companyDto);

            // then
            Assert.Equal(1, context.Companies.Count());
            Assert.Equal(companyDto.Name, context.Companies.First().Name);
            Assert.Equal(2, context.Employees.Count());
            Assert.Equal(companyDto.Profile.RegisteredCapital, context.Profiles.First().RegisteredCapital);
            Assert.Equal(companyDto.Profile.CertId, context.Profiles.First().CertId);
        }

        [Fact]
        public async Task Should_get_all_companies_via_company_service()
        {
            // given

            // when
            await companyService.AddCompany(companyDto);
            await companyService.AddCompany(companyDto);
            var companies = await companyService.GetAll();

            // then
            Assert.Equal(2, companies.Count);
        }

        [Fact]
        public async Task Should_get_company_by_id_via_company_service()
        {
            // given

            // when
            var id = await companyService.AddCompany(companyDto);
            var company = await companyService.GetById(id);

            // then
            Assert.Equal(companyDto.Name, company.Name);
            Assert.Equal(companyDto.Profile.CertId, company.Profile.CertId);
            Assert.Equal(companyDto.Employees.Count, company.Employees.Count);
        }

        [Fact]
        public async Task Should_delete_company_by_id_via_company_service()
        {
            // given

            // when
            await companyService.AddCompany(companyDto);
            await companyService.DeleteCompany(context.Companies.First().Id);

            // then
            Assert.Equal(0, context.Companies.Count());
            Assert.Equal(0, context.Employees.Count());
            Assert.Equal(0, context.Profiles.Count());
        }
    }
}
