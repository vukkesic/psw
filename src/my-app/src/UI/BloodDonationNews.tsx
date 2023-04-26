import React, { useEffect, useState } from "react";
import { BloodDonationNotification } from "../Models/BloodDonationNotification";
import { Notification } from "../Models/Notification";
import "./HomePage.css";
import axios from "axios";
import NewNotification from "./NewNotification";

const BloodDonationNews = () => {
    const [bloodDonationNotifications, setBloodDonationNotifications] = useState<BloodDonationNotification[]>();

    const postNotification = (bdn: BloodDonationNotification) => {
        var notification = { id: 0, title: bdn.title, text: bdn.text }
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

    const getAllBloodDonationNotifications = () => {
        axios.get('http://localhost:16177/api/BloodDonation', {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data)
                setBloodDonationNotifications(response.data.reverse());
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    useEffect(() => {
        getAllBloodDonationNotifications();
    }, []);


    return (
        <div className="card-columns">
            <h1>Blood donation news</h1>
            {bloodDonationNotifications?.map((notification, index) =>

                <div className="card border-0">
                    <div className="position-relative">

                    </div>
                    <div className="card-body">
                        <h5>{`${notification.title}`}</h5>
                        <p className="card-text-more">
                            {`${notification.text}`}
                        </p>
                        <p className="card-text">
                            {`Start time: ${notification.startTime}`}
                        </p>
                        <p className="card-text">
                            {`End time: ${notification.endTime}`}
                        </p>
                        <p className="card-text">
                            {`Location: ${notification.location}`}
                        </p>
                    </div>
                    <div className="card-footer">
                        <div className="media align-items-center">
                            <div className="media-body"><a className="card-link text-uppercase" onClick={() => { postNotification(notification) }}>Post notification</a></div>
                        </div>
                    </div>

                </div>
            )
            }
        </div>
    )
}

export default BloodDonationNews;