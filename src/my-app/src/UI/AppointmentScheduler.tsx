import React, { FC, useEffect, useState } from "react";
import { Appointment, Suggestion } from "../Models/Appointment";
import { Priority } from "../Models/Priority";
import { Doctor } from "../Models/User";
import axios from "axios";
import moment from "moment";
import DateTimePicker from 'react-datetime-picker';
import AppointmentModal from "./AppointmentModal";
import AppointmentNotFoundModal from "./AppointmentNotFoundModal";
import { ReferralLetter } from "../Models/ReferralLetter";
import { Examination } from "../Models/Examination";
import RegistrationCheckModal from "./FeedbackModal";

const AppointmentScheduler: FC = () => {
    const [startTime, setStartTime] = useState<Date>();
    const [endTime, setEndTime] = useState<Date>();
    const [doctors, setDoctors] = useState<Doctor[]>([]);
    const [selectedDoctor, setSelectedDoctor] = useState<Doctor>();
    const [selectedPriority, setSelectedPriority] = useState<Priority>();
    const [recommendedAppointment, setRecommendedAppointment] = useState<Suggestion>();
    const current = new Date();
    current.setDate(current.getDate() + 2);
    const [dateCheck, setDateCheck] = useState<boolean>(false);
    const [showAppointment, setShowAppointment] = useState<boolean>(false);
    const [scheduledAppointments, setScheduledAppointments] = useState<Appointment[]>([]);
    const [selctedCancelAppointment, setSelectedCancelAppointment] = useState<Appointment>();
    const [myRefferalLetters, setMyReferralLetters] = useState<ReferralLetter[]>([]);
    const [selectedReferralLetter, setSelectedReferralLetter] = useState<ReferralLetter>();
    const [examinations, setExaminations] = useState<Examination[]>([]);
    const [showErrorModal, setShowErrorModal] = useState<boolean>(false);
    const [showSuccessModal, setShowSuccessModal] = useState<boolean>(false);
    const [errorMessage, setErrorMessage] = useState<string>("");

    const getAllDoctors = () => {
        var spec: Number = 6;
        if (selectedReferralLetter) {
            spec = selectedReferralLetter.specializationId;
        }
        axios.get('http://localhost:16177/api/Users/getAllSpecialist', {
            params: { specializationId: spec },
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data)
                setDoctors(response.data);
                setSelectedDoctor(response.data[0]);
            })
            .catch(function (error) {
                console.log(error);
            });
    }
    const selectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const value = event.target.value;
        setSelectedPriority(+value);
    };

    const submitHandler = (event: React.MouseEvent) => {
        event.preventDefault();
        let data = { startTime: startTime?.toJSON(), endTime: endTime?.toJSON(), doctorId: selectedDoctor?.id, patientId: localStorage.id, priority: selectedPriority };
        console.log(data);
        if (dateCheck) {
            axios.post('http://localhost:16177/api/Appointments/checkPeriod', data,
                {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
                    }
                })
                .then(function (response) {
                    setRecommendedAppointment(response.data);
                    setShowAppointment(true);
                    console.log(response.data);
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
        else {
            setErrorMessage("Period is not valid, please change start and end time and try again.");
            setShowErrorModal(true);
        }
    }

    const getPatientExaminations = () => {
        axios.get('http://localhost:16177/api/Examinations/getExamination', {
            params: { patientId: localStorage.id },
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                setExaminations(response.data);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    const validatePeriod = (st?: Date, et?: Date) => {
        setDateCheck(false);
        if (st != null && et != null && moment(et).isAfter(moment(st))) {
            setDateCheck(true);
        }

    }

    const submitAppointment = () => {
        let refId = selectedReferralLetter?.id;
        const newAppointment = { id: 0, startTime: recommendedAppointment?.startTime, endTime: recommendedAppointment?.endTime, doctorId: recommendedAppointment?.doctorId, patientId: recommendedAppointment?.patientId, canceled: false, cancelationDate: new Date(), used: false }
        axios.post('http://localhost:16177/api/Appointments/addAppointment', newAppointment,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
                }
            })
            .then(function (response) {
                console.log(response);
                getAllDoctors();
                getPatientAppointments();
                getMyReferralLetters();
                getPatientExaminations();
            })
            .catch(function (error) {
                console.log(error);
            });

        if (selectedReferralLetter) {

            axios.put('http://localhost:16177/api/ReferralLetters/' + refId, { id: refId, patientId: selectedReferralLetter.patientId, isActive: false, specializationId: selectedReferralLetter.specializationId }, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
                }
            })
                .then(function (response) {
                    console.log(response);
                    getAllDoctors();
                    getPatientAppointments();
                    getMyReferralLetters();
                    getPatientExaminations();
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    }

    const CancelationHandler = () => {
        if (selctedCancelAppointment) {
            const idsa = selctedCancelAppointment.id;
            axios.put(`http://localhost:16177/api/Appointments/cancel/${idsa}`, {},
                {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
                    }
                })
                .then(function (response) {
                    console.log(response.data);
                    setShowSuccessModal(true);
                    getAllDoctors();
                    getPatientAppointments();
                    getMyReferralLetters();
                    getPatientExaminations();
                })
                .catch(function (error) {
                    console.log(error);
                    setErrorMessage(error.response.data);
                    setShowErrorModal(true);
                });
        }
    }

    const getPatientAppointments = () => {
        axios.get('http://localhost:16177/api/Appointments/getAppointmentsByPatient', {
            params: { patientId: localStorage.id },
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                setScheduledAppointments(response.data);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    const getMyReferralLetters = () => {
        axios.get('http://localhost:16177/api/ReferralLetters/getMyReferralLetters', {
            params: { patientId: localStorage.id },
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data)
                setMyReferralLetters(response.data);
            })
            .catch(function (error) {
                console.log(error);

            });
    }

    useEffect(() => {
        getAllDoctors();
        getPatientAppointments();
        getMyReferralLetters();
        getPatientExaminations();
    }, []);

    useEffect(() => {
        validatePeriod(startTime, endTime);
    }, [startTime, endTime]);

    useEffect(() => {
        getAllDoctors();
    }, [selectedReferralLetter]);

    return (
        <div style={{ display: 'flex', justifyContent: 'space-around', }}>
            <div style={styles.container}>
                <label>
                    Working hours: Mon-Fri 8-16H
                </label>
                <label>
                    Referral letter
                </label>
                <select onChange={event => {
                    setSelectedReferralLetter(JSON.parse(event.target.value));
                }} style={styles.select}>
                    <option value='null'></option>
                    {myRefferalLetters.map((letter, index) =>
                        <option key={index}
                            value={JSON.stringify(letter)}>
                            {letter.specializationId.toString()}
                        </option>
                    )}
                </select>
                <label>
                    start time
                </label>
                <DateTimePicker
                    onChange={setStartTime}
                    value={startTime}
                    minDate={current}
                />
                <label>
                    End time
                </label>
                <DateTimePicker
                    onChange={setEndTime}
                    value={endTime}
                    minDate={startTime}
                />
                <label>
                    Doctor
                </label>
                <select onChange={event => {
                    setSelectedDoctor(JSON.parse(event.target.value));
                }} style={styles.select}>
                    {doctors.map((doctor, index) =>
                        <option key={index}
                            value={JSON.stringify(doctor)}>
                            {doctor.name}
                        </option>
                    )}
                </select>
                <label>
                    Priority
                </label>
                <select onChange={selectChange} style={styles.select}>
                    <option value={Priority.Time}>Time</option>
                    <option value={Priority.Doctor}>Doctor</option>
                </select>
                <button onClick={submitHandler}>
                    submit
                </button>
                {showAppointment && recommendedAppointment &&
                    <AppointmentModal
                        startDate={recommendedAppointment.startTime}
                        endDate={recommendedAppointment.endTime}
                        doctorName={recommendedAppointment.doctorName}
                        message={recommendedAppointment.message}
                        isOpen={showAppointment} setIsOpen={setShowAppointment}
                        createAppointment={submitAppointment} />}
                {showAppointment && !recommendedAppointment &&
                    <AppointmentNotFoundModal
                        message={"Appointment for given parameters were not found.Please adjust your parameters and try again."}
                        isOpen={showAppointment} setIsOpen={setShowAppointment} />}
            </div>
            <div className="card-container">
                <h3>Scheduled Appointments:</h3>
                <ol>
                    {scheduledAppointments.map((appointment, index) =>

                        <li key={index} >
                            <h6>{`Id: ${appointment.id}`}</h6>
                            <h6>{`Start time :  ${appointment.startTime}`}</h6>
                            <h6>{`End time:  ${appointment.endTime}`}</h6>
                            <h6>{`Doctor id:  ${appointment.doctorId}`}</h6>
                        </li>
                    )}
                </ol>
            </div>
            <div className="card-container">
                <h3>Past appointments and examination reports:</h3>
                <ol>
                    {examinations.map((examination, index) =>

                        <li key={index} >
                            <h6>{`Id: ${examination.id}`}</h6>
                            <h6>{`Date :  ${examination.date}`}</h6>
                            <h6>{`Diagnosis code :  ${examination.diagnosisCode}`}</h6>
                            <h6>{`Diagnosis description:  ${examination.diagnosisDescription}`}</h6>
                            <h6>{`Prescription:  ${examination.prescription}`}</h6>
                        </li>
                    )}
                </ol>
            </div>
            <div style={styles.container}>
                <label>
                    Cancel appointment
                </label>
                <select onChange={event => {
                    setSelectedCancelAppointment(JSON.parse(event.target.value));
                }} style={styles.select}>
                    <option value='null'></option>
                    {scheduledAppointments.map((app, index) =>
                        <option key={index}
                            value={JSON.stringify(app)}>
                            {"Id" + app.id.toString()}
                        </option>
                    )}
                </select>
                <button onClick={CancelationHandler}>Cancel appointment</button>
            </div>
            {showErrorModal && <RegistrationCheckModal
                errMessage={errorMessage}
                isOpen={showErrorModal}
                setIsOpen={setShowErrorModal} />}
            {showSuccessModal && <RegistrationCheckModal
                errMessage={"Selected appointment is successfully canceled."}
                isOpen={showSuccessModal}
                setIsOpen={setShowSuccessModal} />}
        </div >
    )
}

export default AppointmentScheduler;

const styles: { [name: string]: React.CSSProperties } = {
    container: {
        marginTop: 50,
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
    },
    select: {
        padding: 5,
        width: 200,
    },
    result: {
        marginTop: 30,
    },
};