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
    errMessage: string,
    isOpen: boolean,
    setIsOpen: Function,
}

const RegistrationCheckModal = (props: Props) => {
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
                    <h4>{`${props.errMessage}`}</h4>
                    <div className="buttons">
                        <button className="primary ghost" onClick={() => closeModal()}>
                            Close
                        </button>
                    </div>
                </div>
            </Modal>
        </div>
    );
}

export default RegistrationCheckModal;