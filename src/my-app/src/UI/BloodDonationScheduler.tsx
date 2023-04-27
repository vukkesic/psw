import axios from "axios";
import React, { FC, useEffect, useState } from "react";
import { BloodDonationNotification } from "../Models/BloodDonationNotification";
import { Patient } from "../Models/User";
import { HealthData } from "../Models/HealthData";
import { MenstrualData } from "../Models/MenstrualData";

const BloodDonationScheduler: FC = () => {
    const [bloodDonationNotifications, setBloodDonationNotifications] = useState<BloodDonationNotification[]>([]);
    const [patients, setPatients] = useState<Patient[]>([]);
    const [selectedPatient, setSelectedPatient] = useState<Patient>();
    const [selectedBloodDonationNotification, setSelectedBloodDonationNotification] = useState<BloodDonationNotification>();
    const [healthData, setHealthData] = useState<HealthData[]>();
    const [menstrualData, setMenstrualData] = useState<MenstrualData[]>();

    const getAllBloodDonationNotifications = () => {
        axios.get('http://localhost:16177/api/BloodDonation', {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data)
                setBloodDonationNotifications(response.data.reverse());
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    const getAllPatients = () => {
        axios.get('http://localhost:16177/api/Users/getAllPatients', {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data)
                setPatients(response.data());
                setSelectedPatient(response.data[0]);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    const submitHandler = (event: React.MouseEvent) => {
        event.preventDefault();
    }

    const checkHealthCondition = () => {

    }

    const getLastTwoDaysHealthData = (patientId: Number) => {
        axios.get('http://localhost:16177/api/HealthData/getLastTwoDaysHealthData', {
            params: { patientId: patientId },
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data)
                setHealthData(response.data());
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    const getMenstrualData = (patientId: Number) => {
        axios.get('http://localhost:16177/api/HealthData/getLastTwoDaysHealthData', {
            params: { patientId: patientId },
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data)
                setMenstrualData(response.data());
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    useEffect(() => {
        getAllBloodDonationNotifications();
        getAllPatients();
    })
    return (
        <div style={styles.container}>
            <label>
                Patient
            </label>
            <select onChange={event => {
                setSelectedPatient(JSON.parse(event.target.value));
            }} style={styles.select}>
                {patients.map((patient, index) =>
                    <option key={index}
                        value={JSON.stringify(patient)}>
                        {`${patient.name} + ${patient.surname}`}
                    </option>
                )}
            </select>
            <label>
                Blood donation action
            </label>
            <select onChange={event => {
                setSelectedBloodDonationNotification(JSON.parse(event.target.value));
            }} style={styles.select}>
                <option value='null'></option>
                {bloodDonationNotifications.map((bdn, index) =>
                    <option key={index}
                        value={JSON.stringify(bdn)}>
                        {`${bdn.startTime.toString()}, ${bdn.location}`}
                    </option>
                )}
            </select>
            <button onClick={submitHandler}>
                submit
            </button>
        </div>
    )
}

export default BloodDonationScheduler;

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