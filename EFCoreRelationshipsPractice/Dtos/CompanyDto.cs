using System.Collections.Generic;
using System.Linq;
using EFCoreRelationshipsPractice.Entities;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(CompanyEntity companyEntity)
        {
            this.Name = companyEntity.Name;
            this.Profile = companyEntity.Profile == null ? null : new ProfileDto(companyEntity.Profile);
            this.Employees = companyEntity.Employees.Select(companyEntity => new EmployeeDto(companyEntity)).ToList();
        }

        public string Name { get; set; }

        public ProfileDto Profile { get; set; }

        public List<EmployeeDto> Employees { get; set; }

        //public override bool Equals(object? obj)
        //{
        //    return Equals((CompanyDto)obj);
        //}

        //public bool Equals(CompanyDto companyDto)
        //{
        //    return companyDto.Name == Name && companyDto.Profile.Equals(Profile) && companyDto.Employees.Equals(Employees);
        //}
    }
}