using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            ProfileEntity = companyDto.Profile == null ? null : new ProfileEntity(companyDto.Profile);
            Employees = companyDto.Employees?.Select(employeeDto => new EmployeeEntity(employeeDto)).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ProfileEntity ProfileEntity { get; set; }
        [ForeignKey("CompanyId")]
        public ICollection<EmployeeEntity> Employees { get; set; }
    }
}
