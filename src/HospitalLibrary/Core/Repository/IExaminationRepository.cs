﻿using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public interface IExaminationRepository
    {
        IEnumerable<ExaminationReport> GetAll();
        ExaminationReport GetById(int id);
        void Create(ExaminationReport examinationReport);
        void Update(ExaminationReport examinationReport);
        void Delete(ExaminationReport examinationReport);
        IEnumerable<ExaminationReport> GetByPatientId(int patientId);
    }
}
