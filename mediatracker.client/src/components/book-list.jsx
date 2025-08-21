import React, { useState, useEffect } from 'react';
import BookItem from './book-item'
import ReactPaginate from 'react-paginate';
import { Container } from 'react-bootstrap';

const BookList = () => {
    const[books, setBooks] = useState(null);
    const[bookCount, setBookCount] = useState(0);
    const[page, setPage] = useState(0);

    useEffect(() => {
        // get all movies
        getBooks();
    }, [page]);

    const getBooks = () => {
        fetch(import.meta.env.VITE_API_URL + "/movie?pageSize=" + import.meta.env.VITE_PAGING_SIZE + "&pageIndex=" + page)
        .then(res => res.json())
        .then(res => {
            if(res.status === true && res.data.count > 0) {
                setBooks(res.data.books)        
                setBookCount(Math.ceil(res.data.count / import.meta.env.VITE_PAGING_SIZE));
            }

            if(res.data.count === 0){
                alert("There is no book data in the system.");
            }
        })
        .catch(err => alert("Error getting data"));
    }

    const handlePageClick = (pageIndex) => {
        setPage(pageIndex.selected);
    }

    return (
        <Container>
            {books && isEmptyObject(books) ?
            books.map((m,i) => <BookItem key={i} data={m} /> ) 
        :""}

        <div className="d-flex justify-content-center">
            <ReactPaginate
                previousLabel={'Previous'}
                nextLabel={'Next'}
                breakLabel={'...'}
                breakClassName={'page-link'}
                pageCount={bookCount}
                marginPageDisplayed={2}
                pageRangeDisplayed={5}
                onPageChange={handlePageClick}
                containerClassName={'pagination'}
                pageClassName={'page-item'}
                pageLinkClassName={'page-link'}
                previousClassName={'page-link'}
                nextClassName={'page-link'}
                activeClassName={'active'}
            />

        </div>
        </Container>
    )

}

export default BookList;