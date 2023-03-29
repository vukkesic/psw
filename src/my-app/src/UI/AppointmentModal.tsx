import React, { FC } from 'react';
import ReactDOM from 'react-dom';
import Modal from 'react-modal';

const customStyles = {
    content: {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)',
    },
};

interface Props {
    startDate: Date,
    endDate: Date,
    doctorName: string,
    message: string,
    isOpen: boolean,
    setIsOpen: Function,
    createAppointment: Function
}

const AppointmentModal = (props: Props) => {
    let subtitle: HTMLHeadingElement;

    function openModal() {
        props.setIsOpen(true);
    }

    function afterOpenModal() {
        // references are now sync'd and can be accessed.

    }

    function closeModal() {
        props.setIsOpen(false);
    }

    return (
        <div>
            <Modal
                isOpen={props.isOpen}
                onAfterOpen={afterOpenModal}
                onRequestClose={closeModal}
                style={customStyles}
                contentLabel="Example Modal"
                ariaHideApp={false}

            >
                <div className="card-container">
                    <h4>{`${props.message}`}</h4>
                    <h6>{`Start time :  ${props.startDate}`}</h6>
                    <h6>{`End time:  ${props.endDate}`}</h6>
                    <h6>{`Doctor name:  ${props.doctorName}`}</h6>
                    <div className="buttons">
                        <button className="primary" onClick={() => {
                            props.createAppointment();
                            closeModal()
                        }}>
                            Confirm
                        </button>
                        <button className="primary ghost" onClick={() => closeModal()}>
                            Reject
                        </button>
                    </div>
                </div>
            </Modal>
        </div>
    );
}

export default AppointmentModal;