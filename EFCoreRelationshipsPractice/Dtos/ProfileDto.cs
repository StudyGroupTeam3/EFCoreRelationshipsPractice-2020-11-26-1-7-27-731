using EFCoreRelationshipsPractice.NewFolder;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class ProfileDto
    {
        public ProfileDto()
        {
        }

        public ProfileDto(CompanyEntity companyEntity)
        {
            CertId = companyEntity.Profile.CertId;
            RegisteredCapital = companyEntity.Profile.RegisteredCapital;
        }

        public int RegisteredCapital { get; set; }
        public string CertId { get; set; }
    }
}