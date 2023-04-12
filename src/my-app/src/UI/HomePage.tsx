import axios from "axios";
import React, { useEffect, useState } from "react";
import { Notification } from "./Notification";
import "./HomePage.css"
import { Blog } from "../Models/Blog";


const HomePage = () => {
    const [notifications, setNotifications] = useState<Notification[]>();
    const [blogPosts, setBlogPosts] = useState<Blog[]>();
    const [readMore, setReadMore] = useState<boolean>(false);
    const [showBlogs, setShowBlogs] = useState<boolean>(false)

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

    const getAllBlogPosts = () => {
        axios.get('http://localhost:16177/api/Blogs')
            .then(function (response) {
                console.log(response.data)
                setBlogPosts(response.data.reverse());
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    useEffect(() => {
        getAllNotifications();
        getAllBlogPosts();
    }, []);


    return (
        <div className="container">
            <label className="toggle" htmlFor="uniqueID">
                <input type="checkbox" onChange={e => setShowBlogs(!showBlogs)} />
                show blogs
            </label>
            {!showBlogs && <div className="card-columns">
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
            </div>}

            {showBlogs && <div className="card-columns">
                <h1>Blog</h1>
                {blogPosts?.map((post, index) =>

                    <div className="card border-0">
                        <div className="position-relative">

                        </div>
                        <div className="card-body">
                            <h5>{`${post.title}`}</h5>
                            {!readMore && <p className="card-text">
                                {`${post.text}`}
                            </p>}
                            {readMore && <p className="card-text-more">
                                {`${post.text}`}
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
            </div>}
        </div >
    )
}

export default HomePage;