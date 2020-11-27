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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((CompanyDto)obj);
        }

        private bool Equals(CompanyDto other)
        {
            return Name == other.Name;
        }
    }
}