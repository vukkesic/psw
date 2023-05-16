import axios from "axios";
import React, { useState } from "react";
import FeedbackModal from "./FeedbackModal";

const NewBlogForm = () => {
    const [inputText, setInputText] = useState<string>("");
    const [inputTitle, setInputTitle] = useState<string>("");
    const [showErrorModal, setShowErrorModal] = useState<boolean>(false);
    const [showSuccessModal, setShowSuccessModal] = useState<boolean>(false);
    const [errMessage, setErrMessage] = useState<string>("");

    const submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (inputText !== '' && inputTitle !== '') {
            let blog = { id: 0, title: inputTitle, text: inputText }
            axios.post('http://localhost:16177/api/Blogs', blog, {
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
                });
        }
        else {
            setErrMessage("You must enter title and text before submitting data.");
            setShowErrorModal(true);
        }
    }
    return (
        <div>
            <section className="blog-form">
                <form className="form-style-5" onSubmit={submitHandler}>
                    <h1>New blog</h1>
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
            {showErrorModal && <FeedbackModal
                errMessage={errMessage}
                isOpen={showErrorModal}
                setIsOpen={setShowErrorModal} />}
            {showSuccessModal && <FeedbackModal
                errMessage={"Successfully added."}
                isOpen={showSuccessModal}
                setIsOpen={setShowSuccessModal} />}
        </div>
    );
}

export default NewBlogForm;