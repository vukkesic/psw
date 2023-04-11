import React, { FC } from "react";
import "./Navbar.css"
import { useNavigate } from "react-router-dom";
import {
    Navbar,
    NavbarBrand,
    Nav,
    NavItem,
    NavLink,
    NavbarText,
} from 'reactstrap';

const NavigationBar: FC = () => {
    const navigate = useNavigate();

    const logout = () => {
        localStorage.removeItem("userToken");
        localStorage.removeItem("id");
        localStorage.removeItem("role");
        console.log(localStorage.getItem("id"));
        navigate("/login")
    }

    return (
        <div>
            <Navbar className="navbar">
                <Nav className="navl" navbar>
                    {localStorage.role == 0 &&
                        <NavItem>
                            <NavLink href="/dashboard">
                                Profile
                            </NavLink>
                        </NavItem>}
                    {localStorage.role == 0 &&
                        <NavItem>
                            <NavLink href="/scheduling">
                                Scheduling
                            </NavLink>
                        </NavItem>}
                    {localStorage.role == 1 &&
                        <NavItem>
                            <NavLink href="/doctorProfile">
                                Profile
                            </NavLink>
                        </NavItem>}
                    {localStorage.role == 1 &&
                        <NavItem>
                            <NavLink href="/newblog">
                                New blog post
                            </NavLink>
                        </NavItem>}
                    {localStorage.role == 1 &&
                        <NavItem>
                            <NavLink href="/examination">
                                Start examination
                            </NavLink>
                        </NavItem>}
                    {localStorage.role == 2 &&
                        <NavItem>
                            <NavLink href="/newnotification">
                                New notification
                            </NavLink>
                        </NavItem>}
                    {localStorage.role == 2 &&
                        <NavItem>
                            <NavLink href="/blocking">
                                Block user
                            </NavLink>
                        </NavItem>}
                </Nav>
                <Nav className="nav" navbar>
                    <NavbarBrand className="brand" href="/">Home</NavbarBrand>
                </Nav>
                <Nav className="navr" navbar>
                    {localStorage.id == null &&
                        <NavItem>
                            <NavLink href="/login/">
                                Login
                            </NavLink>
                        </NavItem>}
                    {localStorage.id == null &&
                        <NavItem>
                            <NavLink href="/signup">
                                Signup
                            </NavLink>
                        </NavItem>}
                    {localStorage.id != null &&
                        <NavbarText onClick={logout}>Logout</NavbarText>}
                </Nav>
            </Navbar>
        </div>
    );
}

export default NavigationBar;