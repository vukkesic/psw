import React, { FC, useEffect, useState } from "react";
import Moment from "react-moment";
import Calendar from "react-calendar";
import "./MenstrualCalendar.css"
import "react-calendar/dist/Calendar.css";
import { useNavigate } from 'react-router-dom';
import moment from "moment";
import axios from "axios";
import { MenstrualData } from "../Models/MenstrualData";


const MenstrualCalendar: FC = () => {
    const [cycle, cycleValue] = useState("");
    const [myMenstrualData, setMyMenstrualData] = useState<MenstrualData>();
    const [selectedLastPeriod, setSelectedLastPeriod] = useState(myMenstrualData?.lastPeriod);
    const [selectedNextPeriod, setSelectedNextPeriod] = useState(myMenstrualData?.nextPeriod);
    const [SelectedApproxOvulationDay, setSelectedApproxOvulationDate] = useState(myMenstrualData?.approxOvulationDay);
    const navigate = useNavigate();
    const [lastCycle, setLastCycle] = useState("");

    const date = selectedLastPeriod;
    console.log(cycle);
    const cycleLength = parseInt(cycle);

    const getMenstrualDataByPatientId = (patientId: Number) => {
        axios.get('http://localhost:16177/api/MenstrualData/getMyMenstrualData', {
            params: { patientId: localStorage.id },
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data)
                setMyMenstrualData(response.data);
                setSelectedLastPeriod(new Date(response.data.lastPeriod));
                setSelectedNextPeriod(new Date(response.data.nextPeriod));
                const diffTime = (new Date(response.data.nextPeriod).getTime() - new Date(response.data.lastPeriod).getTime()) / (1000 * 60 * 60 * 24) + 1;
                setLastCycle(diffTime.toString());
                cycleValue(diffTime.toString());
            })
            .catch(function (error) {
                console.log(error);

            });
    }

    useEffect(() => {
        getMenstrualDataByPatientId(localStorage.id);
    }, []);

    useEffect(() => {
        setSelectedNextPeriod(moment(selectedLastPeriod).add(cycleLength - 1, 'days').add(2, 'hours').toDate());
        setSelectedApproxOvulationDate(moment(selectedLastPeriod).add(cycleLength - 1 - 14, 'days').add(2, 'hours').toDate());
    }, [selectedLastPeriod, cycleLength]);

    const submitHandler = () => {
        if (selectedLastPeriod !== undefined) {
            const idmd = myMenstrualData?.id;
            const lastPeriodFixed = new Date(selectedLastPeriod?.getTime() + 2 * 60 * 60 * 1000);
            let data = { id: idmd, lastPeriod: lastPeriodFixed, nextPeriod: selectedNextPeriod, approxOvulationDay: SelectedApproxOvulationDay, patientId: myMenstrualData?.patientId }
            axios.put(`http://localhost:16177/api/MenstrualData/${idmd}`, data,
                {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
                    }
                })
                .then(function (response) {
                    console.log(response.data)
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    }

    return (
        <div>
            <h4>Calculate Next period, Ovulation Day</h4>

            <label >Select your Cycle Length : </label>
            <select
                onChange={(e) => cycleValue(e.target.value)}
                defaultValue={cycle}
                className="m-2"
            >
                <option value={lastCycle}>unchanged</option>
                <option value="28">28</option>
                <option value="29">29</option>
                <option value="30">30</option>
                <option value="31">31</option>
                <option value="32">32</option>
                <option value="33">33</option>
                <option value="34">34</option>
                <option value="35">35</option>
                <option value="36">36</option>
                <option value="37">37</option>
                <option value="38">38</option>
                <option value="39">39</option>
                <option value="40">40</option>
                <option value="41">41</option>
                <option value="42">42</option>
                <option value="43">43</option>
                <option value="44">44</option>
                <option value="45">45</option>
            </select>
            <p className="text-center">
                Select Your Last Period Start Date from the Calendar
            </p>
            <div className="d-flex justify-content-center ">
                <Calendar onChange={setSelectedLastPeriod} value={selectedLastPeriod}
                    maxDate={new Date()}
                    className="calendar mt-0" />
            </div>
            <div className="text-center mt-4 p-2">
                <div className="row">
                    <div className="d-flex justify-content-center">
                        <div className="col-md-3 m-3 box ">
                            <p>Next Period</p>

                            <Moment format="Do MMMM YYYY"
                                add={{ days: cycleLength - 1 }}>
                                {date}
                            </Moment>
                        </div>
                        <div className="col-md-3 m-3 box ">
                            <p> Approximate Ovulation Day</p>

                            <Moment
                                format="Do MMMM YYYY"
                                add={{ days: cycleLength - 1 - 14 }}
                            >
                                {date}
                            </Moment>
                        </div>
                    </div>
                </div>
            </div>
            <button onClick={() => navigate(-1)}>go back</button>
            <button onClick={submitHandler}>submit</button>
        </div >
    )
}

export default MenstrualCalendar;