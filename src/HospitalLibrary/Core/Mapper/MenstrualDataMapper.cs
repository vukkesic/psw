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
    public class MenstrualDataMapper
    {
        private readonly IPatientService _patientService;
        public MenstrualDataMapper(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public MenstrualDataDTO MapMenstrualDataToDTO(MenstrualData data)
        {
            return new MenstrualDataDTO(data.Id, data.LastPeriod.ToLocalTime(), data.NextPeriod.ToLocalTime(), data.ApproxOvulationDay.ToLocalTime(), data.PatientId);
        }

        public MenstrualData MapDTOToMenstrualData(MenstrualDataDTO dto)
        {
            return new MenstrualData(dto.Id, dto.LastPeriod, dto.NextPeriod, dto.ApproxOvulationDay, dto.PatientId, _patientService.GetById(dto.PatientId));
        }
    }
}
