using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EFCoreRelationshipsPractice.NewFolder
{
    public class CompanyEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ProfileEntity Profile { get; set; }
    }

    public class ProfileEntity
    {
        public int ID { get; set; }
        public int RegisteredCapital { get; set; }
        public string CertId { get; set; }
    }
}
