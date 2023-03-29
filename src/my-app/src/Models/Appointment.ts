import { Priority } from "./Priority"

export type AppointmentPeriod = {
    startTime: Date,
    endTime: Date,
    doctorId: Number,
    priority: Priority
}

export type Appointment = {
    id: Number,
    startTime: Date,
    endTime: Date,
    doctorId: Number,
    patientId: Number,
    canceled: boolean,
    cancelationTime: Date,
    used: boolean
}

export type Suggestion = {
    startTime: Date,
    endTime: Date,
    doctorId: Number,
    patientId: Number,
    doctorName: string,
    message: string
}