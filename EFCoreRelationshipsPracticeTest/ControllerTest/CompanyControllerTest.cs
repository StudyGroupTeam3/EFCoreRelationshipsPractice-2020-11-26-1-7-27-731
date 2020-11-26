using EFCoreRelationshipsPractice;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EFCoreRelationshipsPracticeTest
{
    public class CompanyControllerTest : TestBase
    {
        public CompanyControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_company_employee_profile_success()
        {
            // given
            var client = GetClient();
            var companyDto = new CompanyDto
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>() { new EmployeeDto() { Name = "Tom", Age = 19 } },
                Profile = new ProfileDto() { RegisteredCapital = 100010, CertId = "100", },
            };

            // when
            var httpContent = JsonConvert.SerializeObject(companyDto);
            var content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/companies", content);

            var allCompaniesResponse = await client.GetAsync("/companies");
            var body = await allCompaniesResponse.Content.ReadAsStringAsync();
            var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

            // then
            Assert.Equal(1, returnCompanies.Count);
            Assert.Equal(companyDto.Employees.Count, returnCompanies[0].Employees.Count);
            Assert.Equal(companyDto.Employees[0].Age, returnCompanies[0].Employees[0].Age);
            Assert.Equal(companyDto.Employees[0].Name, returnCompanies[0].Employees[0].Name);
            Assert.Equal(companyDto.Profile.CertId, returnCompanies[0].Profile.CertId);
            Assert.Equal(companyDto.Profile.RegisteredCapital, returnCompanies[0].Profile.RegisteredCapital);
        }

        [Fact]
        public async Task Should_delete_company_and_related_employee_and_profile_success()
        {
            // given
            var client = GetClient();
            var companyDto = new CompanyDto
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>() { new EmployeeDto() { Name = "Tom", Age = 19 } },
                Profile = new ProfileDto() { RegisteredCapital = 100010, CertId = "100", },
            };

            // when
            var httpContent = JsonConvert.SerializeObject(companyDto);
            var content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/companies", content);

            await client.DeleteAsync(response.Headers.Location);

            var allCompaniesResponse = await client.GetAsync("/companies");
            var body = await allCompaniesResponse.Content.ReadAsStringAsync();
            var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

            // then
            Assert.Equal(0, returnCompanies.Count);
        }

        [Fact]
        public async Task Should_create_many_companies_success()
        {
            // given
            var client = GetClient();
            var companyDto = new CompanyDto
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>() { new EmployeeDto() { Name = "Tom", Age = 19 } },
                Profile = new ProfileDto() { RegisteredCapital = 100010, CertId = "100", },
            };

            // when
            var httpContent = JsonConvert.SerializeObject(companyDto);
            var content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            await client.PostAsync("/companies", content);
            await client.PostAsync("/companies", content);

            var allCompaniesResponse = await client.GetAsync("/companies");
            var body = await allCompaniesResponse.Content.ReadAsStringAsync();
            var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

            Assert.Equal(2, returnCompanies.Count);
        }

        [Fact]
        public async Task Should_create_company_employee_profile_success_via_company_service()
        {
            // given
            var scope = Factory.Services.CreateScope();
            var scopeService = scope.ServiceProvider;
            var context = scopeService.GetRequiredService<CompanyDbContext>();
            var companyService = new CompanyService(context);
            var companyDto = new CompanyDto
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>()
                {
                    new EmployeeDto() { Name = "Tom", Age = 19 },
                    new EmployeeDto() { Name = "Tom2", Age = 20 },
                },
                Profile = new ProfileDto() { RegisteredCapital = 100010, CertId = "100", },
            };

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
            var scope = Factory.Services.CreateScope();
            var scopeService = scope.ServiceProvider;
            var context = scopeService.GetRequiredService<CompanyDbContext>();
            var companyService = new CompanyService(context);
            var companyDto = new CompanyDto
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>()
                {
                    new EmployeeDto() { Name = "Tom", Age = 19 },
                    new EmployeeDto() { Name = "Tom2", Age = 20 },
                },
                Profile = new ProfileDto() { RegisteredCapital = 100010, CertId = "100", },
            };

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
            var scope = Factory.Services.CreateScope();
            var scopeService = scope.ServiceProvider;
            var context = scopeService.GetRequiredService<CompanyDbContext>();
            var companyService = new CompanyService(context);
            var companyDto = new CompanyDto
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>()
                {
                    new EmployeeDto() { Name = "Tom", Age = 19 },
                    new EmployeeDto() { Name = "Tom2", Age = 20 },
                },
                Profile = new ProfileDto() { RegisteredCapital = 100010, CertId = "100", },
            };

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
            var scope = Factory.Services.CreateScope();
            var scopeService = scope.ServiceProvider;
            var context = scopeService.GetRequiredService<CompanyDbContext>();
            var companyService = new CompanyService(context);
            var companyDto = new CompanyDto
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>()
                {
                    new EmployeeDto() { Name = "Tom", Age = 19 },
                    new EmployeeDto() { Name = "Tom2", Age = 20 },
                },
                Profile = new ProfileDto() { RegisteredCapital = 100010, CertId = "100", },
            };

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