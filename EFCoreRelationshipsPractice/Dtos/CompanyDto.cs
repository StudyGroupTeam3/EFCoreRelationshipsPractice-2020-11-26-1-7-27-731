using System.Collections.Generic;
using System.Linq;
using EFCoreRelationshipsPractice.Entities;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(CompanyEntity companyEntity)
        {
            Name = companyEntity.Name;
            Profile = companyEntity.ProfileEntity == null ? null : new ProfileDto(companyEntity.ProfileEntity);
            Employees = companyEntity.Employees?.Select(employeeEntity => new EmployeeDto(employeeEntity)).ToList();
        }

        public string Name { get; set; }

        public ProfileDto Profile { get; set; }

        public List<EmployeeDto> Employees { get; set; }
    }
}