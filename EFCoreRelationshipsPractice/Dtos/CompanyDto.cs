using System.Collections.Generic;
using System.Linq;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(Entities.CompanyEntity companyEntity)
        {
            this.Name = companyEntity.Name;
            this.Profile = companyEntity.ProfileEntity == null ? null : new ProfileDto(companyEntity.ProfileEntity);
            this.Employees = companyEntity.Employees.Select(employeeEntity => new EmployeeDto(employeeEntity)).ToList();
        }

        public string Name { get; set; }
        public ProfileDto Profile { get; set; }
        public List<EmployeeDto> Employees { get; set; }
    }
}