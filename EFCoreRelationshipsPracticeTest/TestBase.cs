﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
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
            Factory = factory;
        }

        protected CustomWebApplicationFactory<Startup> Factory { get; }

        public void Dispose()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<CompanyDbContext>();

            context.Companies.RemoveRange(context.Companies);
            context.Profiles.RemoveRange(context.Profiles);
            context.Employees.RemoveRange(context.Employees);

            context.SaveChanges();
        }

        protected HttpClient GetClient()
        {
            return Factory.CreateClient();
        }
    }
}