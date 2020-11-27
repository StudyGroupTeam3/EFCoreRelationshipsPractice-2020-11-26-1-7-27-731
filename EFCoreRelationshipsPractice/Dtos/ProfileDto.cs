using System;
using EFCoreRelationshipsPractice.Entities;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class ProfileDto
    {
        public ProfileDto()
        {
        }

        public ProfileDto(ProfileEntity profileEntity)
        {
            this.RegisteredCapital = profileEntity.RegisteredCapital;
            this.CertId = profileEntity.CertId;
        }

        public int RegisteredCapital { get; set; }
        public string CertId { get; set; }
    }
}