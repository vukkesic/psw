import axios from "axios";
import React, { FC, useEffect, useState } from "react";
import { Appointment } from "../Models/Appointment";

const Examination: FC = () => {
    const [diagnosisCode, setDiagnosisCode] = useState<string>('');
    const [diagnosisDescription, setDiagnosisDescription] = useState<string>('');
    const [prescription, setPrescription] = useState<string>('');
    const [myAppointments, setMyAppointments] = useState<Appointment[]>([]);
    const [selectedAppointment, setSelectedAppointment] = useState<Appointment>();
    const [showDialog, setShowDialog] = useState<boolean>(false);
    const [presureHigh, setPresureHigh] = useState<string>('');
    const [presureLow, setPresureLow] = useState<string>('');
    const [bloodSugar, setBloodSugar] = useState<string>('');
    const [bodyFatPercentage, setBodyFatPercentage] = useState<string>('');
    const [weight, setWeight] = useState<string>('');

    const getMyAppointments = () => {
        axios.get('http://localhost:16177/api/Appointments/getDoctorTodayAppointments',
            {
                params: { today: new Date(), doctorId: localStorage.id },
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
                }
            })
            .then(function (response) {
                console.log(response.data)
                setMyAppointments(response.data);
            }).catch(function (error) {
                console.log(error);
            });
    }

    const submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        console.log(selectedAppointment)
        let bp = `${presureHigh}/${presureLow}`;
        if (selectedAppointment !== undefined) {
            if (showDialog == true) {
                let d = { id: 0, bloodPresure: bp, bloodSugar: bloodSugar, bodyFatPercentage: bodyFatPercentage, weight: weight, userId: selectedAppointment.patientId.toString(), measurementTime: new Date() };
                console.log(d)
                axios.post('http://localhost:16177/api/HealthData', d,
                    {
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
                        }
                    })
                    .then(function (response) {
                        let responseId = response.data.id;
                        let e = { id: 0, diagnosisCode: diagnosisCode, diagnosisDescription: diagnosisDescription, doctorId: selectedAppointment?.doctorId, patientId: selectedAppointment?.patientId, date: new Date(), healthDataId: responseId, prescription: prescription }
                        axios.post('http://localhost:16177/api/Examinations', e, {
                            headers: {
                                'Content-Type': 'application/json',
                                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
                            }
                        })
                            .then(function (response) {
                                console.log(response)
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
            else {
                let responseId = 0;
                let e = { id: 0, diagnosisCode: diagnosisCode, diagnosisDescription: diagnosisDescription, doctorId: selectedAppointment?.doctorId, patientId: selectedAppointment?.patientId, date: new Date(), healthDataId: responseId, prescription: prescription }
                axios.post('http://localhost:16177/api/Examinations', e, {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
                    }
                })
                    .then(function (response) {
                        console.log(response)
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
            const idsa = selectedAppointment.id;
            axios.put(`http://localhost:16177/api/Appointments/use/${idsa}`, {}, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
                }
            })
                .then(function (response) {
                    console.log(response.data)
                })
                .catch(function (error) {
                    console.log(error);

                });
        }
    }

    useEffect(() => {
        getMyAppointments();
    }, []);

    return (
        <div>
            <section className="container" style={{ backgroundColor: '#eee' }}>
                <form className="form-style-5" onSubmit={submitHandler}>
                    <h1>Examination data</h1>
                    <label>
                        Appointment
                    </label>
                    <select onChange={event => {
                        setSelectedAppointment(JSON.parse(event.target.value));
                    }} style={styles.select}>
                        <option value='null'></option>
                        {myAppointments.map((appointment, index) =>
                            <option key={index}
                                value={JSON.stringify(appointment)}>
                                {appointment.startTime.toString()}
                            </option>
                        )}
                    </select>
                    <label></label>
                    <label>
                        Diagnosis id
                    </label>
                    <input
                        placeholder="Enter diagnosis id"
                        type="text"
                        value={diagnosisCode}
                        onChange={event => {
                            setDiagnosisCode(event.target.value);
                        }}
                    />
                    <label >
                        Diagnosis description
                    </label>
                    <textarea
                        placeholder="Enter diagnosis description"
                        value={diagnosisDescription}
                        onChange={event => {
                            setDiagnosisDescription(event.target.value)
                        }}
                    />
                    <label >
                        Prescription
                    </label>
                    <textarea
                        placeholder="Enter diagnosis description"
                        value={prescription}
                        onChange={event => {
                            setPrescription(event.target.value)
                        }}
                    />
                    <label>
                        <input
                            type="checkbox"
                            checked={showDialog}
                            onChange={() => setShowDialog(!showDialog)}
                        />
                        Health data required
                    </label>
                    <div className="login-form__actions">
                        <button
                            type="submit"
                            color="primary"
                        >
                            Submit
                        </button>
                    </div>
                </form>
                {showDialog === true &&

                    <form className="form-style-5">
                        <label>
                            Blood pressure(mmHg)
                        </label>
                        <div style={{ display: 'flex' }}>
                            <input style={{ width: '45%' }}
                                max={400}
                                min={30}
                                placeholder="Presure high"
                                type="number"
                                value={presureHigh}
                                onChange={event => {
                                    setPresureHigh(event.target.value);
                                }}
                            />
                            <label style={{ width: '10%', padding: '10px' }}>
                                /
                            </label>
                            <input style={{ width: '45%' }}
                                max={400}
                                min={30}
                                placeholder="Presure low"
                                type="number"
                                value={presureLow}
                                onChange={event => {
                                    setPresureLow(event.target.value);
                                }}
                            />
                        </div>
                        <label>
                            Blood sugar(mmol/L)
                        </label>
                        <input
                            max={50}
                            min={0}
                            placeholder="Blood sugar"
                            type="number"
                            value={bloodSugar}
                            onChange={event => {
                                setBloodSugar(event.target.value);
                            }}
                        />
                        <label>
                            Body fat percentage(%)
                        </label>
                        <input
                            max={70}
                            min={0}
                            placeholder="bodyFatPercentage"
                            type="number"
                            value={bodyFatPercentage}
                            onChange={event => {
                                setBodyFatPercentage(event.target.value)
                            }}
                        />
                        <label>
                            Weight(kg)
                        </label>
                        <input
                            max={999}
                            min={0}
                            placeholder="weight"
                            type="number"
                            value={weight}
                            onChange={event => {
                                setWeight(event.target.value)
                            }}
                        />
                    </form>
                }
            </section>
        </div>
    )
}

// Styling 
const styles = {
    container: {
        backgroundColor: '#fff9c4',
        padding: '10px 50px 60px 50px'
    },
    listItem: {
        borderTop: '1px dashed #ccc'
    },
    select: {
        padding: 5,
        width: 200,
    }
}

export default Examination;