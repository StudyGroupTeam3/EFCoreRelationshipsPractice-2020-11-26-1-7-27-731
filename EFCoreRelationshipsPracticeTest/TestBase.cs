using System;
using System.Net.Http;
using EFCoreRelationshipsPractice;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EFCoreRelationshipsPracticeTest
{
    public class TestBase : IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {
        public TestBase(CustomWebApplicationFactory<Startup> factory)
        {
            this.Factory = factory;
        }

        protected CustomWebApplicationFactory<Startup> Factory { get; }

        public void Dispose() //  继承了IDisposable， 跑Xunit的时候，每次测试都会调Dispose方法，可以在Dispose方法中clean Evironment
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<CompanyDbContext>();

            context.Companies.RemoveRange(context.Companies);

            context.Employees.RemoveRange(context.Employees);

            context.SaveChanges();
        }

        protected HttpClient GetClient()
        {
            return Factory.CreateClient();
        }
    }
}