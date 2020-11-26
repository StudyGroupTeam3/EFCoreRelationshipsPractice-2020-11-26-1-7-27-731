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
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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
            //Given
            var client = GetClient();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.Employees = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19
                }
            };

            companyDto.Profile = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };

            var httpContent = JsonConvert.SerializeObject(companyDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            //When
            await client.PostAsync("/companies", content);
            var allCompaniesResponse = await client.GetAsync("/companies");
            var body = await allCompaniesResponse.Content.ReadAsStringAsync();

            var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

            //Then
            Assert.Equal(1, returnCompanies.Count);
            Assert.Equal(companyDto.Employees.Count, returnCompanies[0].Employees.Count);
            Assert.Equal(companyDto.Employees[0].Age, returnCompanies[0].Employees[0].Age);
            Assert.Equal(companyDto.Employees[0].Name, returnCompanies[0].Employees[0].Name);
            Assert.Equal(companyDto.Profile.CertId, returnCompanies[0].Profile.CertId);
            Assert.Equal(companyDto.Profile.RegisteredCapital, returnCompanies[0].Profile.RegisteredCapital);
            //var scope = Factory.Services.CreateScope();
            //var scopedServices = scope.ServiceProvider;
            //var context = scopedServices.GetRequiredService<CompanyDbContext>();
        }

        [Fact]
        public async Task Should_delete_company_and_related_employee_and_profile_success()
        {
            //Givem
            var client = GetClient();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.Employees = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19
                }
            };

            companyDto.Profile = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };

            var httpContent = JsonConvert.SerializeObject(companyDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await client.PostAsync("/companies", content);

            //When
            await client.DeleteAsync(response.Headers.Location);
            var allCompaniesResponse = await client.GetAsync("/companies");
            var body = await allCompaniesResponse.Content.ReadAsStringAsync();

            var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

            //Then
            Assert.Equal(0, returnCompanies.Count);
        }

        [Fact]
        public async Task Should_create_many_companies_success()
        {
            //Given
            var client = GetClient();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.Employees = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19
                }
            };

            companyDto.Profile = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };

            var httpContent = JsonConvert.SerializeObject(companyDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/companies", content);
            await client.PostAsync("/companies", content);

            //When
            var allCompaniesResponse = await client.GetAsync("/companies");
            var body = await allCompaniesResponse.Content.ReadAsStringAsync();

            var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

            //Then
            Assert.Equal(2, returnCompanies.Count);
        }

        [Fact]
        public async Task Should_create_company_success_via_company_service()
        {
            //Given
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            CompanyDbContext context = scopedServices.GetRequiredService<CompanyDbContext>();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.Employees = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19
                }
            };

            companyDto.Profile = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };

            CompanyService companyService = new CompanyService(context);

            //When
            await companyService.AddCompany(companyDto);

            //Then
            Assert.Equal(1, context.Companies.Count());
        }

        [Fact]
        public async Task Should_Get_company_By_ID_success_via_company_service()
        {
            //Given
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            CompanyDbContext context = scopedServices.GetRequiredService<CompanyDbContext>();

            CompanyService companyService = new CompanyService(context);
            List<CompanyDto> companyDtos = GiveMeSomeCompanies();
            foreach (var company in companyDtos)
            {
                await companyService.AddCompany(company);
            }

            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "Alibaba";
            companyDto.Employees = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Jordan",
                    Age = 18
                }
            };

            companyDto.Profile = new ProfileDto()
            {
                RegisteredCapital = 423455,
                CertId = "234",
            };
            var testID = await companyService.AddCompany(companyDto);

            //When
            CompanyDto actualCompany = await companyService.GetById(testID);

            //Then
            Assert.Equal(companyDto, actualCompany);
        }

        [Fact]
        public async Task Should_Get_AllCompany_success_via_company_service()
        {
            //Given
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            CompanyDbContext context = scopedServices.GetRequiredService<CompanyDbContext>();

            CompanyService companyService = new CompanyService(context);
            List<CompanyDto> companyDtos = GiveMeSomeCompanies();
            foreach (var company in companyDtos)
            {
                await companyService.AddCompany(company);
            }

            //When
            List<CompanyDto> actualCompanies = await companyService.GetAll();

            //Then
            Assert.Equal(2, actualCompanies.Count);
        }

        [Fact]
        public async Task Should_Delete_Company_ByID_success_via_companyService()
        {
            //Given
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            CompanyDbContext context = scopedServices.GetRequiredService<CompanyDbContext>();

            CompanyService companyService = new CompanyService(context);
            List<CompanyDto> companyDtos = GiveMeSomeCompanies();
            int testID = 0;
            foreach (var company in companyDtos)
            {
                testID = await companyService.AddCompany(company);
            }

            //When
            await companyService.DeleteCompany(testID);

            //Then
            Assert.Equal(1, context.Companies.Count());
        }

        private List<CompanyDto> GiveMeSomeCompanies()
        {
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "FaceBook";
            companyDto.Employees = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 37
                }
            };

            companyDto.Profile = new ProfileDto()
            {
                RegisteredCapital = 1000110,
                CertId = "1020",
            };

            CompanyDto companyDto1 = new CompanyDto();
            companyDto1.Name = "Amazon";
            companyDto1.Employees = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Chris",
                    Age = 37
                },
                new EmployeeDto()
                {
                Name = "Jack",
                Age = 25
                }
            };

            companyDto1.Profile = new ProfileDto()
            {
                RegisteredCapital = 23423,
                CertId = "124345",
            };

            return new List<CompanyDto>() { companyDto, companyDto1 };
        }
    }
}