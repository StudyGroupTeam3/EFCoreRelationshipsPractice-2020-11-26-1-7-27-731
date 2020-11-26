using System.Collections.Generic;
using EFCoreRelationshipsPractice.NewFolder;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(CompanyEntity conCompanyEntity)
        {
            Name = conCompanyEntity.Name;
        }

        public string Name { get; set; }

        public ProfileDto Profile { get; set; }

        public List<EmployeeDto> Employees { get; set; }
    }
}