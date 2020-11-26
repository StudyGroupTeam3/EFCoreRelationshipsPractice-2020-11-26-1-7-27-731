using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;

namespace EFCoreRelationshipsPractice.NewFolder
{
    public class CompanyEntity
    {
        public CompanyEntity()
        {
        }

        public CompanyEntity(CompanyDto companyDto)
        {
            Name = companyDto.Name;
            Profile = new ProfileEntity(companyDto);
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public ProfileEntity Profile { get; set; }
        public List<EmployeeEntity> Emlpoyees { get; set; }
    }

    public class EmployeeEntity
    {
        public EmployeeEntity()
        {
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class ProfileEntity
    {
        public ProfileEntity()
        {
        }

        public ProfileEntity(CompanyDto companyDto)
        {
            RegisteredCapital = companyDto.Profile.RegisteredCapital;
            CertId = companyDto.Profile.CertId;
        }

        public int ID { get; set; }
        public int RegisteredCapital { get; set; }
        public string CertId { get; set; }
    }
}
