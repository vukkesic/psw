import React, { FC, useState } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import { Button, Input } from "reactstrap"
import { Gender } from "../Models/Gender";
import moment from "moment";
import { Role } from "../Models/Role";
import axios from "axios";
import "./Form.css"

const RegistrationForm: FC = () => {
    const [inputEmail, setInputEmail] = useState<string>('');
    const [inputPassword, setInputPassword] = useState<string>('');
    const [inputDateOfBirth, setInputDateOfBirth] = useState<Date>(new Date());
    const [inputName, setInputName] = useState<string>('');
    const [inputSurname, setInputSurname] = useState<string>('');
    const [inputGender, setInputGender] = useState<Gender>(Gender.Male);
    const [inputPhone, setInputPhone] = useState<string>('');
    const [inputProfileImage, setInputProfileImage] = useState<File>();
    const [inputProfileImageName, setInputProfileImageName] = useState<string>('');

    const uploadImage = (e: React.ChangeEvent<HTMLInputElement>) => {
        e.preventDefault();
        const target = e.target;
        if (!target.files) {
            console.log('error');
        }
        else {
            const file = target.files[0];
            setInputProfileImage(file);
            setInputProfileImageName(file.name)
            console.log(file);
        }
    }

    const cancelImage = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        setInputProfileImage(undefined);
        setInputProfileImageName('');
    }

    const uploadHandler = async (e: React.MouseEvent<HTMLButtonElement>) => {
        const formData = new FormData();
        formData.append("formFile", inputProfileImage!);
        formData.append("fileName", inputProfileImageName);
        try {
            const res = await axios.post("http://localhost:16177/api/Users/uploadImage", formData);
            console.log(res);
        } catch (ex) {
            console.log(ex);
        }
    }

    const submitHandler = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let imagePath = '';
        if (inputProfileImage !== undefined) {
            const formData = new FormData();
            formData.append("formFile", inputProfileImage!);
            formData.append("fileName", inputProfileImageName);
            try {
                const res = await axios.post("http://localhost:16177/api/Users/uploadImage", formData);
                imagePath = res.data;
                console.log(res);
            } catch (ex) {
                console.log(ex);
            }
            let data = { id: 0, name: inputName, surname: inputSurname, dateOfBirth: inputDateOfBirth, email: inputEmail, username: inputEmail, password: inputPassword, phone: inputPhone, gender: inputGender, profileImageName: imagePath, role: Role.Patient };
            axios.post('http://localhost:16177/api/Users/userRegistration', data)
                .then(function (response) {
                    console.log(response)
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
        else {
            let imagePath = '';
            let data = { id: 0, name: inputName, surname: inputSurname, dateOfBirth: inputDateOfBirth, email: inputEmail, username: inputEmail, password: inputPassword, phone: inputPhone, gender: inputGender, profileImageName: imagePath, role: Role.Patient };
            axios.post('http://localhost:16177/api/Users/userRegistration', data)
                .then(function (response) {
                    console.log(response)
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    }

    return (
        <section className="signup-form">
            <div className="card">
                <form className="form-style-5" onSubmit={submitHandler}>
                    <label>
                        Name
                    </label>
                    <input
                        placeholder="Name"
                        type="text"
                        value={inputName}
                        onChange={event => {
                            setInputName(event.target.value);
                        }}
                    />
                    <label>
                        Surname
                    </label>
                    <input
                        placeholder="Surname"
                        type="text"
                        value={inputSurname}
                        onChange={event => {
                            setInputSurname(event.target.value);
                        }}
                    />
                    <label >
                        Date of birth
                    </label>
                    <input
                        id="dateOfBirth"
                        name="date"
                        placeholder="date placeholder"
                        type="date"
                        max={moment().format("YYYY-MM-DD")}
                        onChange={event => {
                            let date = new Date(event.target.value);
                            setInputDateOfBirth(date);
                        }}
                    />
                    <label>
                        Email
                    </label>
                    <input
                        placeholder="example@gmail.com"
                        type="email"
                        value={inputEmail}
                        onChange={event => {
                            setInputEmail(event.target.value);
                        }}
                    />
                    <label >
                        Password
                    </label>
                    <input
                        placeholder="Your password"
                        type="password"
                        value={inputPassword}
                        onChange={event => {
                            setInputPassword(event.target.value)
                        }}
                    />
                    <label >
                        Phone number
                    </label>
                    <input
                        id="phoneNumber"
                        name="number"
                        placeholder="phone number"
                        type="number"
                        value={inputPhone}
                        onChange={event => {
                            setInputPhone(event.target.value);
                        }}
                    />
                    <label>
                        Gender
                    </label>
                    <Input
                        type="select"
                        value={inputGender}
                        onChange={event => {
                            setInputGender(+event.target.value);
                        }}
                    >
                        <option value={Gender.Male}>
                            Male
                        </option>
                        <option value={Gender.Female}>
                            Female
                        </option>
                    </Input>
                    <label >
                        Image
                    </label>
                    <input
                        type="file"
                        onChange={(e) => uploadImage(e)}
                    />
                    <button onClick={(e) => cancelImage(e)}>X</button>
                    <div className="signup-form__actions">
                        <button
                            type="submit"
                            color="primary"
                        >
                            Sign Up
                        </button>
                    </div>
                </form>
            </div>

        </section>


    );
};

export default RegistrationForm;
