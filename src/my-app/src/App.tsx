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
import Dashboard from './UI/Dashboard';
import ChartDisplay from './UI/ChartDisplay';
import AppointmentScheduler from './UI/AppointmentScheduler';
import MenstrualCalendar from './UI/MenstrualCalendar';
import Examination from './UI/Examination';
import DoctorProfile from './UI/DoctorProfile';

function App() {
  return (
    <div className="App">
      <Router>
        <NavigationBar />
        <Routes>
          <Route path="/signup" element={<RegistrationForm />} />
          <Route path="/login" element={<LoginForm />} />
          <Route path='/dashboard' element={localStorage.role == 0 ? <Dashboard /> : <Navigate to="/" />} />
          <Route path='/scheduling' element={localStorage.role == 0 ? <AppointmentScheduler /> : <Navigate to="/" />} />
          <Route path='dashboard/chart' element={localStorage.role == 0 ? <ChartDisplay /> : <Navigate to="/" />} />
          <Route path='/calendar' element={localStorage.role == 0 ? <MenstrualCalendar /> : <Navigate to="/" />} />
          <Route path='/doctorProfile' element={localStorage.role == 1 ? <DoctorProfile /> : <Navigate to="/" />} />
          <Route path='/examination' element={localStorage.role == 1 ? <Examination /> : <Navigate to="/" />} />
          <Route path='examination/chart' element={localStorage.role == 1 ? <ChartDisplay /> : <Navigate to="/" />} />
          <Route path="/*" element={<HomePage />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
