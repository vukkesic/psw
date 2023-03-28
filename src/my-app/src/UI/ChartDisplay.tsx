import React from 'react';
import { useLocation } from 'react-router-dom';
import { Chart } from "./Chart";

const ChartDisplay = () => {
    const location = useLocation();
    const { state } = location;

    return (
        <section style={{ backgroundColor: '#eee' }}>
            <div>
                {Chart(state.arrayDates, state.arrayHigh, state.arrayLow, state.arraySugar, state.arrayFat, state.arrayWeight)}
            </div>
        </section>
    )
}

export default ChartDisplay;