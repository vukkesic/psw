import { Gender } from "./Gender";
import { Role } from "./Role";

export type RegisterData = {
    id: number,
    name: string,
    surname: string,
    dateOfBirth: Date
    email: string,
    username: string
    password: string,
    phone: string
    gender: Gender,
    profileImageName: string,
    role: Role
};

export type LoginData = {
    username: string,
    password: string
};

export type HealthData = {
    id: number,
    bloodPresure: string,
    bloodSugar: string,
    bodyFatPercentage: string,
    weight: string,
    userId: string,
    measurementTime: Date
};