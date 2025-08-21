import { useEffect, useState } from 'react';
import './App.css';
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import { Container, Navbar, NavbarBrand, Nav } from 'react-bootstrap';

import Landing from './pages/landing';
import Series from './pages/series';

function App() {
    return (
        <Container>
            <BrowserRouter>
                <Navbar bg="dark" variant="dark">
                    <Navbar.Brand as={Link} to="/">Library</Navbar.Brand>
                    <Nav className="mr-auto">
                        <Nav.Link as={Link} to="/">Books</Nav.Link>
                        <Nav.Link as={Link} to="/series">Series</Nav.Link>
                    </Nav>
                </Navbar>
                <Routes>
                    <Route path="/" element={<Landing />} />
                    <Route path="/series" element={<Series />} />
                </Routes>
            </BrowserRouter>
        </Container>
    );
}


export default App;