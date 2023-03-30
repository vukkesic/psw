using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IReferralLetterService
    {
        IEnumerable<ReferralLetter> GetAll();
        ReferralLetter GetById(int id);
        void Create(ReferralLetter referralLetter);
        void Update(ReferralLetter referralLetter);
        void Delete(ReferralLetter referralLetter);
        public IEnumerable<ReferralLetter> GetMyReferralLetters(int patientId);
    }
}
