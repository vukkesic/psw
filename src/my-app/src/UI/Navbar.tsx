import React, { FC } from "react";
import "./Navbar.css"
import { useNavigate } from "react-router-dom";
import {
    Navbar,
    NavbarBrand,
    Nav,
    NavItem,
    NavLink,
} from 'reactstrap';

const NavigationBar: FC = () => {
    const navigate = useNavigate();
    return (
        <div>
            <Navbar className="navbar">
                <Nav className="nav" navbar>
                    <NavbarBrand className="brand" href="/">Home</NavbarBrand>
                </Nav>
                <Nav>
                    <NavItem>
                        <NavLink href="/signup">
                            Signup
                        </NavLink>
                    </NavItem>
                </Nav>
            </Navbar>
        </div>
    );
}

export default NavigationBar;