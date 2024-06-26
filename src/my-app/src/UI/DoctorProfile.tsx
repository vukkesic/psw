import React, { useEffect, useState } from "react";
import { Gender } from "../Models/Gender";
import axios from "axios";
import "./UserProfile.css";
import men from "../assets/men.jpg"
import women from "../assets/women.jpg"

const DoctorProfile = () => {
    const [userId, setUserId] = useState<string>(localStorage.id);
    const [name, setName] = useState<string>('');
    const [surname, setSurname] = useState<string>('');
    const [email, setEmail] = useState<string>('');
    const [phone, setPhone] = useState<string>('');
    const [gender, setGender] = useState<Gender>();
    const [genderString, setGenderString] = useState<string>('');
    const [profileImage, setProfileImage] = useState<string>('');
    const [licenseNumber, setLicenseNumber] = useState<string>('');
    const [specilaization, setSpecialization] = useState<Number>();
    const [specializationString, setSpecializationString] = useState<string>('');

    const getCurrentDoctor = (doctorId: Number) => {
        axios.get(`http://localhost:16177/api/Users/${doctorId}`, {
            params: { id: doctorId }, headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                axios.get(`http://localhost:16177/api/Specializations/getSpecializationById`, {
                    params: { id: response.data.specializationId }, headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
                    }
                })
                    .then(function (response) {
                        setSpecializationString(response.data.specName)
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
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
                setLicenseNumber(response.data.licenseNumber);
                setSpecialization(response.data.specilaizationId);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    useEffect(() => {
        getCurrentDoctor(localStorage.id);
    }, []);

    return (
        <section className="container" style={{ backgroundColor: '#eee' }}>
            <div className="card-container">
                <span className="pro">Doctor</span>
                {gender === 0 && <img className="round" src={men} alt={men} />}
                {gender === 1 && <img className="round" src={women} alt={men} />}
                <h3>{name} {surname}</h3>
                <h6>{email}</h6>
                <p>{phone} <br /> {genderString} <br /> {specializationString} </p>
            </div>
        </section>
    )
}

export default DoctorProfile;