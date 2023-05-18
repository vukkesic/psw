import React, { useEffect, useState } from "react";
import { BloodDonationNotification } from "../Models/BloodDonationNotification";
import { Notification } from "../Models/Notification";
import "./HomePage.css";
import axios from "axios";
import NewNotification from "./NewNotification";
import FeedbackModal from "./FeedbackModal";

const BloodDonationNews = () => {
    const [bloodDonationNotifications, setBloodDonationNotifications] = useState<BloodDonationNotification[]>();
    const [showApprovedModal, setShowApprovedModal] = useState<boolean>(false);
    const [showDeniedModal, setShowDeniedModal] = useState<boolean>(false);


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

    const getAllPendingBloodDonationNotifications = () => {
        axios.get('http://localhost:16177/api/BloodDonation/getPending', {
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

    const approveHandler = (notification: BloodDonationNotification) => {
        axios.put(`http://localhost:16177/api/BloodDonation/approve/${notification.id}`, {}, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data);
                postNotification(notification);
                setShowApprovedModal(true);
                getAllPendingBloodDonationNotifications();
            })
            .catch(function (error) {
                console.log(error);

            });


    }

    useEffect(() => {
        getAllPendingBloodDonationNotifications();
    }, []);


    function denyHandler(notification: BloodDonationNotification) {
        axios.put(`http://localhost:16177/api/BloodDonation/deny/${notification.id}`, {}, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data);
                setShowDeniedModal(true);
                getAllPendingBloodDonationNotifications();
            })
            .catch(function (error) {
                console.log(error);

            });
    }

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
                            <div className="media-body"><a className="card-link text-uppercase" onClick={() => { approveHandler(notification) }}>Approve action</a></div>
                            <div className="media-body"><a className="card-link text-uppercase" onClick={() => { denyHandler(notification) }}>Deny action</a></div>
                        </div>
                    </div>

                </div>
            )
            }
            {showApprovedModal && <FeedbackModal
                errMessage={"Blood donation action approved."}
                isOpen={showApprovedModal}
                setIsOpen={setShowApprovedModal} />}
            {showDeniedModal && <FeedbackModal
                errMessage={"Blood donation action denied."}
                isOpen={showDeniedModal}
                setIsOpen={setShowDeniedModal} />}
        </div>
    )
}

export default BloodDonationNews;