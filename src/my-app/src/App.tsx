import React from 'react';
import logo from './logo.svg';
import './App.css';
import RegistrationForm from './UI/RegistrationForm';
import {
  BrowserRouter as Router,
  Routes, //replaces "Switch" used till v5
  Route,
  Navigate
} from "react-router-dom";
import HomePage from './UI/HomePage';
import NavigationBar from './UI/Navbar';
import LoginForm from './UI/LoginForm';

function App() {
  return (
    <div className="App">
      <Router>
        <NavigationBar />
        <Routes>
          <Route path="/signup" element={<RegistrationForm />} />
          <Route path="/login" element={<LoginForm />} />
          <Route path="/*" element={<HomePage />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
