import React, { FC, useState } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import { Form, Button, FormGroup, Label, Input } from 'reactstrap';
import { LoginData } from "../Models/User";
import { useNavigate } from "react-router-dom";
import "./Form.css"
import axios from "axios";

const LoginForm: FC = () => {
    const [inputEmail, setInputEmail] = useState('');
    const [inputPassword, setInputPassword] = useState('');
    const navigate = useNavigate();

    const submitHandlerLogin = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        axios.post('http://localhost:16177/api/Authentication/login', ({ username: inputEmail, password: inputPassword }))
            .then(function (response) {
                console.log(response);
                if (response.data.token) {
                    localStorage.setItem("userToken", JSON.stringify(response.data.token));
                    localStorage.setItem("id", JSON.stringify(response.data.id));
                    localStorage.setItem("role", JSON.stringify(response.data.role));
                    console.log(localStorage);
                    navigate("/dashboard");

                }
            })
            .catch(function (error) {
                console.log(error);
            });


    }
    return (
        <div>
            <section className="login-form">
                <form className="form-style-5" onSubmit={submitHandlerLogin}>

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
                    <div className="login-form__actions">
                        <Button
                            type="submit"
                            color="primary"
                        >
                            Log in
                        </Button>
                    </div>
                </form>
            </section>
        </div>
    );
}

export default LoginForm;