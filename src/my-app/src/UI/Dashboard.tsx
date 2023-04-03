import React, { FC, useEffect, useState } from "react";
import axios from "axios";
import { Gender } from "../Models/Gender";
import "./UserProfile.css";
import men from "../assets/men.jpg"
import women from "../assets/women.jpg"
import { HealthData } from "../Models/HealthData";
import { useNavigate } from "react-router-dom";

const Dashboard: FC = () => {
    const [userId, setUserId] = useState<string>(localStorage.id);
    const [name, setName] = useState<string>('');
    const [surname, setSurname] = useState<string>('');
    const [email, setEmail] = useState<string>('');
    const [phone, setPhone] = useState<string>('');
    const [gender, setGender] = useState<Gender>();
    const [genderString, setGenderString] = useState<string>('');
    const [profileImage, setProfileImage] = useState<string>('');
    const [healthDataId, setHealthDataId] = useState<number>(0);
    const [bloodPresure, setBloodPresure] = useState<string>('');
    const [presureHigh, setPresureHigh] = useState<string>('');
    const [presureLow, setPresureLow] = useState<string>('');
    const [bloodSugar, setBloodSugar] = useState<string>('');
    const [bodyFatPercentage, setBodyFatPercentage] = useState<string>('');
    const [weight, setWeight] = useState<string>('');
    const [editMode, setEditMode] = useState<boolean>(false);
    const [healthData, setHealthData] = useState<HealthData[]>([]);
    const [bloodPresureCheck, setBloodPresureCheck] = useState<boolean>(false);
    const listId: Number[] = [];
    const listPresureHigh: string[] = [];
    const listPresureLow: string[] = [];
    const listBloodSugar: string[] = [];
    const listBodyFatPercentage: string[] = [];
    const listWeight: string[] = [];
    const listMeasurementTime: Date[] = [];
    const navigate = useNavigate();

    const mapValuesForGraph = () => {
        healthData.forEach((item, index) => {
            listId.push(
                item.id
            );
            listPresureHigh.push(
                item.bloodPresure.split('/', 1)[0]
            );
            listPresureLow.push(
                item.bloodPresure.split('/', 2)[1]
            );
            listBloodSugar.push(
                item.bloodSugar
            );
            listBodyFatPercentage.push(
                item.bodyFatPercentage
            );
            listWeight.push(
                item.weight
            );
            listMeasurementTime.push(
                item.measurementTime
            );
        });
        console.log(listMeasurementTime, listPresureHigh, listPresureLow, listBloodSugar, listBodyFatPercentage, listWeight, listId);
    }

    const getUserData = (userId: Number) => {
        axios.get('http://localhost:16177/api/HealthData/getHealthData', {
            params: { userId: userId }
        })
            .then(function (response) {
                console.log(response.data)
                setHealthData(response.data);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    const getCurrentUser = (userId: Number) => {
        axios.get('http://localhost:16177/api/Users/getCurrentUser', { params: { id: userId } })
            .then(function (response) {
                console.log(response.data)
                setName(response.data.name);
                setSurname(response.data.surname);
                setEmail(response.data.email);
                setPhone(response.data.phone);
                setGender(response.data.gender);
                if (response.data.gender == 0) {
                    setGenderString('Male');
                } else {
                    setGenderString('Female');
                }
                setProfileImage(response.data.profileImage);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    const HealthDataHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (bloodPresureCheck === true) {
            setBloodPresure(`${presureHigh}/${presureLow}`);
            let bp = `${presureHigh}/${presureLow}`;
            let d = { id: healthDataId, bloodPresure: bp, bloodSugar: bloodSugar, bodyFatPercentage: bodyFatPercentage, weight: weight, userId: userId, measurementTime: new Date() };
            console.log(d)
            axios.post('http://localhost:16177/api/HealthData', d)
                .then(function (response) {
                    setEditMode(!editMode);
                    setBloodPresureCheck(!bloodPresureCheck);
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
        else {
            console.log("Presure high must be greater than presure low");
        }
    }

    const showGraph = () => {
        mapValuesForGraph();
        navigate('chart', { state: { arrayDates: listMeasurementTime, arrayHigh: listPresureHigh, arrayLow: listPresureLow, arraySugar: listBloodSugar, arrayFat: listBodyFatPercentage, arrayWeight: listWeight } })
    }

    const BloodPresureCheck = (high: string, low: string) => {
        if (Number(high) > Number(low) && low !== '' && high !== '') {
            setBloodPresureCheck(true);
        }
        else {
            setBloodPresureCheck(false);
        }
    }

    useEffect(() => {
        getCurrentUser(localStorage.id);
        getUserData(localStorage.id);
    }, []);

    useEffect(() => {
        BloodPresureCheck(presureHigh, presureLow);
    }, [presureHigh, presureLow]);

    return (
        <section className="container" style={{ height: '88vh', backgroundColor: '#eee' }}>
            <div className="card-container">
                <span className="pro">Patient</span>
                {gender === 0 && <img className="round" src={men} alt={men} />}
                {gender === 1 && <img className="round" src={women} alt={women} />}
                <h2>{name} {surname}</h2>
                <h4>{email}</h4>
                <p>{phone} <br /> {genderString}</p>
                <div className="buttons">
                    <button className="primary" onClick={() => setEditMode(!editMode)}>
                        New health data
                    </button>
                    <button className="primary ghost" onClick={() => showGraph()}>
                        Show Graph
                    </button>
                    {gender === 1 && <button className="primary" onClick={() => navigate("/calendar")}>
                        Menstrual calendar
                    </button>}
                </div>
            </div>
            {editMode === true ?
                <div className="card">
                    <form className="form-style-5" onSubmit={HealthDataHandler}>
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
                        <button
                            type="submit"
                            color="primary"
                        >
                            Submit
                        </button>

                    </form>

                </div>
                :
                <div className="card-container">
                    <h6>Blood pressure: {bloodPresure} mmHg</h6>
                    <h6>Blood sugar : {bloodSugar} mmol/L</h6>
                    <h6>Weight(kg): {weight} kg</h6>
                    <h6>Body fat percentage: {bodyFatPercentage} %</h6>
                </div>}
        </section>
    )


}

export default Dashboard;