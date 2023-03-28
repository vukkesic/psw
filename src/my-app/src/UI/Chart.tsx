import React from 'react';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';
import { Bar } from 'react-chartjs-2';


ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);

export const options = {
    responsive: true,
    plugins: {
        legend: {
            position: 'top' as const,
        },
        title: {
            display: true,
            text: 'Chart.js Bar Chart',
        },
    },
};

export function Chart(arrayDates: [], arrayHigh: [], arrayLow: [], arraySugar: [], arrayFat: [], arrayWeight: []) {
    const labels = arrayDates;
    console.log(labels)
    const data = {
        labels,
        datasets: [
            {
                label: 'Presure high',
                data: arrayHigh,
                backgroundColor: 'rgba(255, 99, 132, 0.5)',
            },
            {
                label: 'Presure low',
                data: arrayLow,
                backgroundColor: 'rgba(53, 162, 235, 0.5)',
            },
            {
                label: 'Blood sugar',
                data: arraySugar,
                backgroundColor: 'rgba(125, 125, 111, 0.5)',
            },
            {
                label: 'Body fat',
                data: arrayFat,
                backgroundColor: 'rgba(215, 19, 92, 0.5)',
            },
            {
                label: 'Weight',
                data: arrayWeight,
                backgroundColor: 'rgba(35, 13, 189, 0.5)',
            }
        ],
    };
    return <Bar options={options} data={data} />;
}
