using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class ReferralLetterService : IReferralLetterService
    {
        private readonly IReferralLetterRepository _referralLetterRepository;
        public ReferralLetterService(IReferralLetterRepository referralLetterRepository)
        {
            _referralLetterRepository = referralLetterRepository;
        }

        public void Create(ReferralLetter referralLetter)
        {
            _referralLetterRepository.Create(referralLetter);
        }

        public void Delete(ReferralLetter referralLetter)
        {
            _referralLetterRepository.Delete(referralLetter);
        }

        public IEnumerable<ReferralLetter> GetAll()
        {
            return _referralLetterRepository.GetAll();
        }

        public ReferralLetter GetById(int id)
        {
            return _referralLetterRepository.GetById(id);
        }

        public IEnumerable<ReferralLetter> GetMyReferralLetters(int patientId)
        {
            return _referralLetterRepository.GetMyReferralLetters(patientId);
        }

        public void Update(ReferralLetter referralLetter)
        {
            _referralLetterRepository.Update(referralLetter);
        }
    }
}
