using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreRelationshipsPractice.Entities
{
    public class CompanyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ProfileEntity ProfileEntity { get; set; }
    }

    public class ProfileEntity
    {
        public int Id { get; set; }
        public int RegisteredCapital { get; set; }
        public string CertId { get; set; }
    }
}
