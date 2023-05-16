import axios from "axios";
import React, { FC, useEffect, useState } from "react";
import { BloodDonationNotification } from "../Models/BloodDonationNotification";
import { Patient } from "../Models/User";
import { HealthData } from "../Models/HealthData";
import { MenstrualData } from "../Models/MenstrualData";
import { Examination } from "../Models/Examination";
import { Gender } from "../Models/Gender";
import moment from "moment";
import FeedbackModal from "./FeedbackModal";


const BloodDonationScheduler: FC = () => {
    const [bloodDonationNotifications, setBloodDonationNotifications] = useState<BloodDonationNotification[]>([]);
    const [patients, setPatients] = useState<Patient[]>([]);
    const [selectedPatient, setSelectedPatient] = useState<Patient>();
    const [selectedBloodDonationNotification, setSelectedBloodDonationNotification] = useState<BloodDonationNotification>();
    const [healthData, setHealthData] = useState<HealthData[]>();
    const [menstrualData, setMenstrualData] = useState<MenstrualData>();
    const [fluReports, setFluReports] = useState<Examination[]>();
    var error = false;
    var errorMessage = "";
    const [showErrorModal, setShowErrorModal] = useState<boolean>(false);
    const [showSuccessModal, setShowSuccessModal] = useState<boolean>(false);
    const [errMessage, setErrMessage] = useState<string>("");

    const getApprovedBloodDonationNotifications = () => {
        axios.get('http://localhost:16177/api/BloodDonation/getApproved', {
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
            setErrMessage(errorMessage);
            setShowErrorModal(true);
            console.log(errorMessage);
        }
        else {
            console.log("pozivanje grpc servisa");
            const spn = `${selectedPatient?.name} ${selectedPatient?.surname}`
            const bloodDonatioReques = { startTime: selectedBloodDonationNotification?.startTime, endTime: selectedBloodDonationNotification?.endTime, patientName: spn, location: selectedBloodDonationNotification?.location };
            axios.post('http://localhost:16177/api/BloodDonation/makeBloodAppointment', bloodDonatioReques,
                {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
                    }
                })
                .then(function (response) {
                    console.log(response);
                    setShowSuccessModal(true);
                })
                .catch(function (error) {
                    console.log(error);
                    setErrMessage(error.response.data);
                    setShowErrorModal(true);
                });

        }
    }

    const checkHealthCondition = () => {
        if (selectedPatient) {
            getLastTwoDaysHealthData(selectedPatient?.id);
            getLastTwoWeeksFluReports(selectedPatient.id);
            if (selectedPatient.gender === Gender.Female) {
                getMenstrualData(selectedPatient.id);
            }
        }
        if (healthData === undefined || healthData.length === 0) {
            errorMessage = "You have not uploaded your helath data in last two days. Please enter it and try again.";
            error = true;
        }
        else {
            healthData.forEach((hd) => {
                if (parseInt(hd.bloodPresure.split('/', 1)[0]) > 180) {
                    error = true;
                    errorMessage = "Pressure too high in last two days, you can't donate blood.";
                }
                if (parseInt(hd.bloodPresure.split('/', 1)[0]) < 100) {
                    error = true;
                    errorMessage = "Pressure too low in last two days, you can't donate blood.";
                }
                if (parseInt(hd.bodyFatPercentage) > 32) {

                    error = true;
                    errorMessage = "Pressure too low in last two days, you can't donate blood.";
                }
            });

        }

        if (fluReports !== undefined && fluReports?.length !== 0) {
            error = true;
            errorMessage = "You have been cold in last two week, you can't donate blood.";
        }

        if (selectedPatient !== undefined && selectedPatient.gender === Gender.Female) {
            if (menstrualData !== undefined) {
                var d = new Date();
                if (menstrualData.lastPeriod.valueOf() > (d.valueOf() - (2 * 24 * 60 * 60 * 1000)) && (menstrualData.lastPeriod.valueOf() < (d.valueOf() + (2 * 24 * 60 * 60 * 1000)))) {
                    error = true;
                    errorMessage = "You are currently in period, you can't donate blood.";
                }
            }
            else {
                errorMessage = "You have not uploaded your menstrual data. Please enter it and try again";
                error = true;
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
        axios.get('http://localhost:16177/api/MenstrualData/getMyMenstrualData', {
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
        getApprovedBloodDonationNotifications();
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
            {showErrorModal && <FeedbackModal
                errMessage={errMessage}
                isOpen={showErrorModal}
                setIsOpen={setShowErrorModal} />}
            {showSuccessModal && <FeedbackModal
                errMessage={"Selected appointment is successfully created."}
                isOpen={showSuccessModal}
                setIsOpen={setShowSuccessModal} />}
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