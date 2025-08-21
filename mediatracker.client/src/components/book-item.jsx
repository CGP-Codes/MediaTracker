import React from 'react';
import { Container, Row } from 'react-bootstrap';

const BookItem = (props) => {
    return (
        <Row>
            <Col item xs={12} md={10}>
                <div><b>{props.data.name}</b></div>
            </Col>
        
        </Row>
    )

}

export default BookItem;