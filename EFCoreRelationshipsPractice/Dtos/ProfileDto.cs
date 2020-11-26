using EFCoreRelationshipsPractice.Repository;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class ProfileDto
    {
        public ProfileDto()
        {
        }

        public ProfileDto(ProfileEntity profile)
        {
            CertId = profile.CertId;
            RegisteredCapital = profile.RegisteredCapital;
        }

        public int RegisteredCapital { get; set; }
        public string CertId { get; set; }
    }
}