import axios from "axios";
import React, { useEffect, useState } from "react";
import { Notification } from "./Notification";
import "./HomePage.css"


const HomePage = () => {
    const [notifications, setNotifications] = useState<Notification[]>();

    const getAllNotifications = () => {
        axios.get('http://localhost:16177/api/Notification')
            .then(function (response) {
                console.log(response.data)
                setNotifications(response.data);
            })
            .catch(function (error) {
                console.log(error);

            });
    }

    useEffect(() => {
        getAllNotifications();
    }, [])


    return (
        <div className="container">
            <div className="card-columns">

                <div className="card border-0">
                    <div className="position-relative">

                    </div>
                    <div className="card-body">
                        <h5 className="card-title">Your health is our concern</h5>

                        <p className="card-text">Ethic and professionalism are the basis of the guidelines that guide
                            us in our daily work. Your health is our concern, your needs are first for us,
                            and our team of skilled doctors are everyday here for you,
                            working closely together in order to find the best treatment for you..</p>
                    </div>
                    <div className="card-footer">
                        <div className="media align-items-center">
                            <div className="media-body"><a className="card-link text-uppercase" href="javascript://">Read More</a></div>
                        </div>
                    </div>
                </div>

                <div className="card border-0">

                    <div className="card-body">
                        <h5 className="card-title">Quick scheduling</h5>

                        <p className="card-text">With our team, you recieve medical tests and physician consultations
                            in just a few days. This means a diagnosis comes fast, and treatments,
                            surgery included, can be scheduled very quickly. We take care of you
                            during the entire treatment.</p>
                    </div>
                    <div className="card-footer">
                        <div className="media align-items-center">
                            <div className="media-body"><a className="card-link text-uppercase" href="javascript://">Read More</a></div>
                        </div>
                    </div>
                </div>
                {notifications?.map((notification, index) =>

                    <div className="card border-0">
                        <div className="position-relative">

                        </div>
                        <div className="card-body">
                            <h5>{`${notification.title}`}</h5>
                            <p className="card-text">
                                {`${notification.text}`}
                            </p>
                        </div>
                        <div className="card-footer">
                            <div className="media align-items-center">
                                <div className="media-body"><a className="card-link text-uppercase" href="javascript://">Read More</a></div>
                            </div>
                        </div>
                    </div>
                )
                }
            </div>
        </div >
    )
}

export default HomePage;