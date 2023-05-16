export type Examination = {
    id: Number,
    diagnosisCode: string,
    diagnosisDescription: string,
    doctorId: Number,
    patientId: Number,
    date: Date,
    healthDataId: Number,
    prescription: string
}