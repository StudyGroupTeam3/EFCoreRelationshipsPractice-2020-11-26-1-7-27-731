using EFCoreRelationshipsPractice.Dtos;

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
    }
}
