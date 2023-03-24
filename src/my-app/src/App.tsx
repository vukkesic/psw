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

function App() {
  return (
    <div className="App">
      <Router>
        <Routes>
          <Route path="/signup" element={<RegistrationForm />} />
          <Route path="/*" element={<HomePage />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
