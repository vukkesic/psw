import React, { FC, useEffect, useState } from "react";
import { Appointment, Suggestion } from "../Models/Appointment";
import { Priority } from "../Models/Priority";
import { Doctor } from "../Models/User";
import axios from "axios";
import moment from "moment";
import DateTimePicker from 'react-datetime-picker';
import AppointmentModal from "./AppointmentModal";
import AppointmentNotFoundModal from "./AppointmentNotFoundModal";

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



    const getAllDoctors = () => {
        axios.get('http://localhost:16177/api/Users/getAllDoctors')
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
            axios.post('http://localhost:16177/api/Appointments/checkPeriod', data)
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
            console.log("error set valid date")
        }
    }

    const validatePeriod = (st?: Date, et?: Date) => {
        setDateCheck(false);
        if (st != null && et != null && moment(et).isAfter(moment(st))) {
            setDateCheck(true);
        }
    }

    const submitAppointment = () => {
        const newAppointment = { id: 0, startTime: recommendedAppointment?.startTime, endTime: recommendedAppointment?.endTime, doctorId: recommendedAppointment?.doctorId, patientId: recommendedAppointment?.patientId, canceled: false, cancelationDate: new Date(), used: false }
        axios.post('http://localhost:16177/api/Appointments/addAppointment', newAppointment)
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    useEffect(() => {
        getAllDoctors();
    }, []);

    useEffect(() => {
        validatePeriod(startTime, endTime);
    }, [startTime, endTime]);

    return (
        <div style={{ display: 'flex', justifyContent: 'space-around', }}>
            <div style={styles.container}>
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
            </div>
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