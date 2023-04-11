import axios from "axios";
import React, { useState } from "react";

const NewNotification = () => {
    const [inputText, setInputText] = useState<string>("");
    const [inputTitle, setInputTitle] = useState<string>("");

    const submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let notification = { id: 0, title: inputTitle, text: inputText }
        axios.post('http://localhost:16177/api/Notification', notification, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response);
                if (response.data.token) {
                    console.log(response.data);
                }
            })
            .catch(function (error) {
                console.log(error);
            });
    }
    return (
        <div>
            <section className="notification-form">
                <form className="form-style-5" onSubmit={submitHandler}>
                    <h1>New notification</h1>
                    <label>
                        Title
                    </label>
                    <input
                        placeholder="Enter title"
                        type="text"
                        value={inputTitle}
                        onChange={event => {
                            setInputTitle(event.target.value);
                        }}
                    />
                    <label >
                        Text
                    </label>
                    <textarea
                        placeholder="Enter text"
                        value={inputText}
                        onChange={event => {
                            setInputText(event.target.value)
                        }}

                    />
                    <div className="login-form__actions">
                        <button
                            type="submit"
                            color="primary"
                        >
                            Submit
                        </button>
                    </div>
                </form>
            </section>
        </div>
    );
}

export default NewNotification;