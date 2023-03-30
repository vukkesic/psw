using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Mapper
{
    public class ReferralLetterMapper
    {
        private readonly ISpecializationService _specializationService;
        public ReferralLetterMapper(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        public ReferralLetter MapDTOToReferralLetter(ReferralLetterDTO dto)
        {
            return new ReferralLetter(dto.Id, dto.PatientId, dto.IsActive, dto.SpecializationId, _specializationService.GetById(dto.SpecializationId));
        }

        public ReferralLetterDTO MapReferralLetterToDTO(ReferralLetter referralLetter)
        {
            return new ReferralLetterDTO(referralLetter.Id, referralLetter.PatientId, referralLetter.IsActive, referralLetter.SpecializationId);
        }
    }
}
