using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Entities;

namespace EFCoreRelationshipsPractice.Repository
{
    public class ProfileEntity
    {
        public ProfileEntity()
        {
        }

        public ProfileEntity(ProfileDto profileDto)
        {
            CertId = profileDto.CertId;
            RegisteredCapital = profileDto.RegisteredCapital;
        }

        public int Id { get; set; }
        public int RegisteredCapital { get; set; }
        public string CertId { get; set; }
        public CompanyEntity Company { get; set; }
        public int CompanyId { get; set; }
    }
}
