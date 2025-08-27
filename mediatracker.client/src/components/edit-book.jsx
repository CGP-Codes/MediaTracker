import React from 'react';
import { Container } from 'react-bootstrap';

//import NoImage from '../no-image.png';

const EditBook = () => {
    const[book, setBook] = useState(null);
    
    const handleFileUpload = (event) => {
        event.preventDefault();
        var file = event.target.files[0];
        const form = new FormData();
        form.append("imageFile");

        fetch (process.env.VITE_API_URL + "/api/Book/upload-book-cover", {
            method: "POST",
            body: form

        })
        .then(res => res.json())
        .then(res => {
            var da = book;
            da.coverImag = res.profileImage;

            setBook(oldData => {return{...oldData, ...da};});
        })
        .catch(err => alert("Error in file upload"));
    }

    const handleFieldChange = (event) => {
        var da = book;
            da.coverImag = res.profileImage;

            setBook(oldData => {return{...oldData, ...da};});

    }
    
    return (
        <>
            <Form>
                <Form.Group className="d-flex justify-content-center">
                    
                </Form.Group>
                <Form.Group className="d-flex justify-content-center">
                    <div><input type="file" conChange={handleFileUpload}/></div>
                </Form.Group>
                <Form.Group controlId="formbooktitle">
                    <Form.Label>Book Title</Form.Label>
                    <Form.Control name="title" value={book && book.title || ''} required type="text" autoComplete="off" placeholder="Enter Book Title" onChange={handleFieldChange} />
                    <div><input type="file" conChange={handleFileUpload}/></div>
                </Form.Group>

            </Form>

        </>
    )

}

export default EditBook;