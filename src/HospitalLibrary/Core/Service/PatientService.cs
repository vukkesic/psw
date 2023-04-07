﻿using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public Patient Create(Patient patient)
        {
            return _patientRepository.Create(patient);
        }

        public void Delete(Patient patient)
        {
            _patientRepository.Delete(patient);
        }

        public bool ExistsById(int id)
        {
            return _patientRepository.ExistsById(id);
        }
        public IEnumerable<Patient> GetAll()
        {
            return _patientRepository.GetAll();
        }

        public Patient GetById(int id)
        {
            return _patientRepository.GetById(id);
        }

        public void Update(Patient user)
        {
            _patientRepository.Update(user);
        }

        public List<Patient> GetAllBlockablePatients(IEnumerable<Appointment> appointments)
        {
            var listPatientId = new List<int>();
            var listBlockablePatientId = new List<int>();
            var blockablePatients = new List<Patient>();
            foreach (Appointment a in appointments)
            {
                listPatientId.Add(a.PatientId);
            }
            var g = listPatientId.GroupBy(i => i);
            foreach (var grp in g)
            {
                if (grp.Count() > 2)
                {
                    listBlockablePatientId.Add(grp.Key);
                }
            }
            foreach (var pid in listBlockablePatientId)
            {
                if (_patientRepository.GetActivePatientById(pid) != null)
                    blockablePatients.Add(_patientRepository.GetActivePatientById(pid));
            }
            return blockablePatients;

        }
    }
}
