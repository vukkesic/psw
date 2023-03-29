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
    message: string,
    isOpen: boolean,
    setIsOpen: Function,
}

const AppointmentNotFoundModal = (props: Props) => {
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
                    <div className="buttons">
                        <button className="primary ghost" onClick={() => closeModal()}>
                            Cancel
                        </button>
                    </div>
                </div>
            </Modal>
        </div>
    );
}

export default AppointmentNotFoundModal;