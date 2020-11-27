using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;

namespace EFCoreRelationshipsPractice.Entities
{
    public class CompanyEntity
    {
        public CompanyEntity()
        {
        }

        public CompanyEntity(CompanyDto companyDto)
        {
            this.Name = companyDto.Name;
            this.Profile = new ProfileEntity(companyDto.Profile);
            this.Employees = companyDto.Employees.Select(employeeDto => new EmployeeEntity(employeeDto)).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ProfileEntity Profile { get; set; }
        public List<EmployeeEntity> Employees { get; set; }
        public int ProfileId { get; set; }
    }

    public class ProfileEntity
    {
        public ProfileEntity()
        {
        }

        public ProfileEntity(ProfileDto profileDto)
        {
            this.CertId = profileDto.CertId;
            this.RegisteredCapital = profileDto.RegisteredCapital;
        }

        public int Id { get; set; }
        public int RegisteredCapital { get; set; }
        public string CertId { get; set; }
    }

    public class EmployeeEntity
    {
        public EmployeeEntity()
        {
        }

        public EmployeeEntity(EmployeeDto employeeDto)
        {
            this.Name = employeeDto.Name;
            this.Age = employeeDto.Age;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CompanyEntityId { get; set; }
    }
}
