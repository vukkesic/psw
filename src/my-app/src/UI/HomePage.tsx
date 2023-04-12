import axios from "axios";
import React, { useEffect, useState } from "react";
import { Notification } from "./Notification";
import "./HomePage.css"


const HomePage = () => {
    const [notifications, setNotifications] = useState<Notification[]>();
    const [readMore, setReadMore] = useState<boolean>(false);

    const getAllNotifications = () => {
        axios.get('http://localhost:16177/api/Notification')
            .then(function (response) {
                console.log(response.data)
                setNotifications(response.data.reverse());
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    useEffect(() => {
        getAllNotifications();
    }, []);


    return (
        <div className="container">

            <div className="card-columns">
                <h1>Notifications</h1>
                {notifications?.map((notification, index) =>

                    <div className="card border-0">
                        <div className="position-relative">

                        </div>
                        <div className="card-body">
                            <h5>{`${notification.title}`}</h5>
                            {!readMore && <p className="card-text">
                                {`${notification.text}`}
                            </p>}
                            {readMore && <p className="card-text-more">
                                {`${notification.text}`}
                            </p>}
                        </div>
                        <div className="card-footer">
                            <div className="media align-items-center">
                                <div className="media-body"><a className="card-link text-uppercase" onClick={() => { setReadMore(!readMore) }}>Read More</a></div>
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