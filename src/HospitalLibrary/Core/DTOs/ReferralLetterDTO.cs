using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class ReferralLetterDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Boolean IsActive { get; set; }
        public int SpecializationId { get; set; }

        public ReferralLetterDTO() { }

        public ReferralLetterDTO(int id, int patientId, bool isActive, int specializationId)
        {
            Id = id;
            PatientId = patientId;
            IsActive = isActive;
            SpecializationId = specializationId;
        }
    }
}
