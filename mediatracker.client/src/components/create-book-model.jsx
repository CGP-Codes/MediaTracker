import React from 'react';
import { Container, Modal } from 'react-bootstrap';

const CreateBookModel = (props) => {

    return (
        <>
            <Modal show={props.show} onHide={props.handleClose} backdrop="static" keyboard={false} centered size="sm">
                <Modal.Header closeButton>
                    <Modal.Title>Add New Book</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <div>this is the body</div>
                </Modal.Body>
            </Modal>
        </>
    )

}

export default CreateBookModel;