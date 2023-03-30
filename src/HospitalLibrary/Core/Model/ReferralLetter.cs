using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class ReferralLetter
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Boolean IsActive { get; set; }
        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }

        public ReferralLetter() { }

        public ReferralLetter(int id, int patientId, Boolean isActive, int specializationId, Specialization specialization)
        {
            Id = id;
            PatientId = patientId;
            IsActive = isActive;
            SpecializationId = specializationId;
            Specialization = specialization;
        }
    }
}
