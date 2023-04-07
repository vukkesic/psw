import React, { FC, useState } from "react";
import { RegisterData } from "../Models/User";
import axios from "axios";

const Blocking: FC = () => {
    const [blockablePatients, setBlockablePatients] = useState<RegisterData[]>([]);
    const [selectedPatient, setSelectedPatient] = useState<RegisterData>();

    const getBlockableUsers = () => {
        axios.get('http://localhost:16177/api/Users/getBlockablePatients', {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data)
                setBlockablePatients(response.data);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    useState(() => {
        getBlockableUsers();
    });

    const blockingHandler = () => {
        let idsp = selectedPatient?.id;
        axios.put(`http://localhost:16177/api/Users/block/${idsp}`, {}, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.userToken.slice(1, -1)}`
            }
        })
            .then(function (response) {
                console.log(response.data);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    return (
        <div style={styles.container}>
            <label>
                Block user
            </label>
            <select onChange={event => {
                setSelectedPatient(JSON.parse(event.target.value));
            }} style={styles.select}>
                <option value='null'></option>
                {blockablePatients.map((bp, index) =>
                    <option key={index}
                        value={JSON.stringify(bp)}>
                        {bp.name}
                    </option>
                )}
            </select>
            <button onClick={blockingHandler}> Block user </button>
        </div>
    )
}

export default Blocking;

const styles: { [name: string]: React.CSSProperties } = {
    container: {
        marginTop: 50,
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
    },
    select: {
        padding: 5,
        width: 200,
    },
    result: {
        marginTop: 30,
    },
};