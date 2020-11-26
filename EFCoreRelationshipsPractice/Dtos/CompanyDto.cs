using System.Collections.Generic;
using System.Linq;
using EFCoreRelationshipsPractice.NewFolder;

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
            Profile = companyEntity.Profile == null ? null : new ProfileDto(companyEntity);
            Employees = companyEntity.Employees?.Select(employee => new EmployeeDto(employee)).ToList();
        }

        public string Name { get; set; }

        public ProfileDto Profile { get; set; }

        public List<EmployeeDto> Employees { get; set; }
    }
}