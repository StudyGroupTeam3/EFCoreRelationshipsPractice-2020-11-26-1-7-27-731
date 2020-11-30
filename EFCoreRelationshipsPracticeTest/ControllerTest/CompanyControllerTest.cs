using EFCoreRelationshipsPractice;
using EFCoreRelationshipsPractice.Dtos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EFCoreRelationshipsPracticeTest
{
    [Collection("TestClass1")]
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
    }
}