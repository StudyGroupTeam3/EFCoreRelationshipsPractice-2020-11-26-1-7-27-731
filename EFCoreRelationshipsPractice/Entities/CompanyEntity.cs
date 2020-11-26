using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreRelationshipsPractice.Entities
{
    public class CompanyEntity
    {
        public CompanyEntity()
        {
        }

        public CompanyEntity(CompanyDto companyDto)
        {
            Name = companyDto.Name;
            ProfileEntity = new ProfileEntity(companyDto.Profile);
            Employees = companyDto.Employees.Select(employeeDto => new EmployeeEntity(employeeDto)).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ProfileEntity ProfileEntity { get; set; }
        public List<EmployeeEntity> Employees { get; set; }
    }
}
