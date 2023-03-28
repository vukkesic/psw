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
    public class PatientHealthDataMapper
    {
        private readonly IPatientService _userService;

        public PatientHealthDataMapper(IPatientService userService)
        {
            _userService = userService;
        }

        public PatientHealthDataDTO MapDataToDto(PatientHealthData data)
        {
            return new PatientHealthDataDTO(data.Id, data.BloodPresure, data.BloodSugar, data.BodyFatPercentage, data.Weight, data.Patient.Id, data.MeasurementTime);
        }

        public PatientHealthData MapDtoToData(PatientHealthDataDTO dto)
        {
            return new PatientHealthData(dto.Id, dto.BloodPresure, dto.BloodSugar, dto.BodyFatPercentage, dto.Weight, _userService.GetById(dto.UserId), dto.MeasurementTime);
        }
    }
}
