import React, { FC, useEffect, useState } from "react";
import axios from "axios";
import { Gender } from "../Models/Gender";
import "./UserProfile.css";
import men from "../assets/men.jpg"
import women from "../assets/women.jpg"

const Dashboard: FC = () => {
    const [userId, setUserId] = useState<string>(localStorage.id);
    const [name, setName] = useState<string>('');
    const [surname, setSurname] = useState<string>('');
    const [email, setEmail] = useState<string>('');
    const [phone, setPhone] = useState<string>('');
    const [gender, setGender] = useState<Gender>();
    const [genderString, setGenderString] = useState<string>('');
    const [profileImage, setProfileImage] = useState<string>('');

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

    useEffect(() => {
        getCurrentUser(localStorage.id);
    }, []);

    return (
        <section className="container" style={{ height: '88vh', backgroundColor: '#eee' }}>
            <div className="card-container">
                <span className="pro">Patient</span>
                {gender === 0 && <img className="round" src={men} alt={men} />}
                {gender === 1 && <img className="round" src={women} alt={women} />}
                <h2>{name} {surname}</h2>
                <h4>{email}</h4>
                <p>{phone} <br /> {genderString}</p>
            </div>
        </section>
    )


}

export default Dashboard;