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
    public class ExaminationReportMapper
    {
        private readonly IPatientHealthDataService _patientHealthDataService;

        public ExaminationReportMapper(IPatientHealthDataService patientHealthDataService)
        {
            _patientHealthDataService = patientHealthDataService;
        }

        public ExaminationReportDTO MapExaminationReportToExaminationReportDTO(ExaminationReport examinationReport)
        {
            int recordId = examinationReport.HealthDataId == 0 ? 0 : examinationReport.HealthDataId.Value;

            return new ExaminationReportDTO(examinationReport.Id, examinationReport.DiagnosisCode,
                examinationReport.DiagnosisDescription, examinationReport.DoctorId, examinationReport.PatientId,
                examinationReport.Date, recordId, examinationReport.Prescription);
        }

        public ExaminationReport MapExaminationReportDTOToExaminationReport(ExaminationReportDTO examinationReportDTO)
        {
            if (examinationReportDTO.HealthDataId == 0)
            {
                return new ExaminationReport(examinationReportDTO.Id, examinationReportDTO.DiagnosisCode,
                  examinationReportDTO.DiagnosisDescription, examinationReportDTO.DoctorId, examinationReportDTO.PatientId,
                  examinationReportDTO.Date, null, null, examinationReportDTO.Prescription);
            }
            return new ExaminationReport(examinationReportDTO.Id, examinationReportDTO.DiagnosisCode,
                   examinationReportDTO.DiagnosisDescription, examinationReportDTO.DoctorId, examinationReportDTO.PatientId,
                   examinationReportDTO.Date, examinationReportDTO.HealthDataId, _patientHealthDataService.GetById(examinationReportDTO.HealthDataId), examinationReportDTO.Prescription);
        }
    }
}
