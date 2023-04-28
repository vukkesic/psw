import axios from "axios";
import React, { FC, useEffect, useState } from "react";
import { BloodDonationNotification } from "../Models/BloodDonationNotification";
import { Patient } from "../Models/User";
import { HealthData } from "../Models/HealthData";
import { MenstrualData } from "../Models/MenstrualData";
import { Examination } from "../Models/Examination";
import { Gender } from "../Models/Gender";
import moment from "moment";

const BloodDonationScheduler: FC = () => {
    const [bloodDonationNotifications, setBloodDonationNotifications] = useState<BloodDonationNotification[]>([]);
    const [patients, setPatients] = useState<Patient[]>([]);
    const [selectedPatient, setSelectedPatient] = useState<Patient>();
    const [selectedBloodDonationNotification, setSelectedBloodDonationNotification] = useState<BloodDonationNotification>();
    const [healthData, setHealthData] = useState<HealthData[]>();
    const [menstrualData, setMenstrualData] = useState<MenstrualData[]>();
    const [fluReports, setFluReports] = useState<Examination[]>();
    const [error, setError] = useState<boolean>(false);
    const [errorMessage, setErrorMessage] = useState<string>();

    const getAllBloodDonationNotifications = () => {
        axios.get('http://localhost:16177/api/BloodDonation', {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data)
                setBloodDonationNotifications(response.data);
                setSelectedBloodDonationNotification(response.data[0]);
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
                setPatients(response.data);
                setSelectedPatient(response.data[0]);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    const submitHandler = async (event: React.MouseEvent) => {
        event.preventDefault();
        checkHealthCondition();
        if (error) {
            console.log(errorMessage);
        }
        else {
            console.log("pozivanje grpc servisa");
        }
    }

    const checkHealthCondition = async () => {

        setError(false);
        if (selectedPatient) {
            getLastTwoDaysHealthData(selectedPatient?.id);
            getLastTwoWeeksFluReports(selectedPatient.id);
            if (selectedPatient.gender === Gender.Female) {
                getMenstrualData(selectedPatient.id);
            }
        }
        if (healthData === undefined || healthData.length === 0) {
            setError(true);
            setErrorMessage("You have not uploaded your helath data in last two days. Please enter it and try again.");
        }
        else {
            healthData.forEach((hd) => {
                if (parseInt(hd.bloodPresure.split('/', 1)[0]) > 180) {
                    setError(true);
                    setErrorMessage("Pressure too high in last two days, you can't donate blood.");
                }
                if (parseInt(hd.bloodPresure.split('/', 1)[0]) < 100) {
                    setError(true);
                    setErrorMessage("Pressure too low in last two days, you can't donate blood.");
                }
                if (parseInt(hd.bodyFatPercentage) > 32) {
                    setError(true);
                    setErrorMessage("Your body fat percentage is too high, you can't donate blood.");
                }
            });

        }

        if (fluReports !== undefined && fluReports?.length !== 0) {
            setError(true);
            setErrorMessage("You have been cold in last two week, you can't donate blood.");
        }

        if (selectedPatient !== undefined && selectedPatient.gender === Gender.Female) {
            if (menstrualData !== undefined && menstrualData.length !== 0) {
                var d = new Date();
                menstrualData.forEach((md) => {
                    if (md.lastPeriod.getTime() > (d.getTime() - (2 * 24 * 60 * 60 * 1000)) && md.lastPeriod.getTime() > (d.getTime() + (2 * 24 * 60 * 60 * 1000))) {
                        setError(true);
                        setErrorMessage("You are currently in period, you can't donate blood.");
                    }
                })

            }
            else {
                setError(true);
                setErrorMessage("You have not uploaded your menstrual data. Please enter it and try again");
            }
        }
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
                setHealthData(response.data);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    const getLastTwoWeeksFluReports = (patientId: Number) => {
        axios.get('http://localhost:16177/api/Examinations/getLastTwoWeeksFluReports', {
            params: { patientId: patientId },
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data)
                setFluReports(response.data);
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
                setMenstrualData(response.data);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    useEffect(() => {
        getAllBloodDonationNotifications();
        getAllPatients();
    }, []);

    useEffect(() => {
        checkHealthCondition();
    }, [selectedPatient]);


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
                        {`${patient.name} ${patient.surname}`}
                    </option>
                )}
            </select>
            <label>
                Blood donation action
            </label>
            <select onChange={event => {
                setSelectedBloodDonationNotification(JSON.parse(event.target.value));
            }} style={styles.select}>
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