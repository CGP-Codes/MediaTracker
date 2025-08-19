using MediaTracker.Server.Data;
using MediaTracker.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MediaTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BookController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(int pageIndex = 0, int pageSize = 10)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                var bookCount = _context.Books.Count();
                var bookList = _context.Books.Skip(pageIndex * pageSize).Take(pageSize).Select(x => new BookViewListModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    AuthorFirst = x.AuthorFirst,
                    AuthorLast = x.AuthorLast,
                    SeriesId = x.SeriesId,
                }).ToList();

                response.Status = true;
                response.Message = "Success";
                response.Data = new { Books = bookList, Count = bookCount };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // TODO: log exceptions
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                var book = _context.Books.Where(x => x.Id == id).Select(x => new BookDetailsViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    AuthorFirst = x.AuthorFirst,
                    AuthorLast = x.AuthorLast,
                    SeriesId = x.SeriesId,
                    ISBN = x.ISBN,
                    PublicationDate = x.PublicationDate,
                    Publisher = x.Publisher
                }).FirstOrDefault();

                if (book == null) 
                { 
                    response.Status = false;
                    response.Message = "Book does not exist";

                    return BadRequest(response);
                }

                response.Status = true;
                response.Message = "Success";
                response.Data = book;

                return Ok(response);
            }
            catch (Exception ex)
            {
                // TODO: log exceptions
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest(response);
            }
        }

        [HttpPost]
        public IActionResult Post(CreateBookViewModel model) 
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                if (ModelState.IsValid) 
                {
                    // This may not work - there will be books that have no series 
                    var series = _context.aSeries.Where(x => model.Series.Equals(x.Id)).FirstOrDefault();

                    if (series == null)
                    {
                        // figure out how to handle stand-alone books
                    }

                    var postedModel = new Entities.Book()
                    {
                        Name = model.Name,
                        AuthorFirst = model.AuthorFirst,
                        AuthorLast = model.AuthorLast,
                        ISBN = model.ISBN,
                        PublicationDate = model.PublicationDate,
                        Publisher = model.Publisher,
                        Series = series
                    };

                    _context.Books.Add(postedModel);
                    _context.SaveChanges();

                    var responseData = new BookDetailsViewModel
                    {
                        Id = postedModel.Id,
                        Name = postedModel.Name,
                        AuthorFirst = postedModel.AuthorFirst,
                        AuthorLast = postedModel.AuthorLast,
                        SeriesId = postedModel.SeriesId,
                        ISBN = postedModel.ISBN,
                        PublicationDate = postedModel.PublicationDate,
                        Publisher = postedModel.Publisher
                    };

                    response.Status = true;
                    response.Message = "Created successfully";
                    response.Data = responseData;

                    return Ok(response);
                }
                else 
                {
                    response.Status = false;
                    response.Message = "Validation failed";
                    response.Data = ModelState;

                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest(response);
            }
        }

        [HttpPut]
        public IActionResult Put(CreateBookViewModel model)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                // Book Id must be valid
                if (model.Id <= 0)
                {
                    response.Status = false;
                    response.Message = "Invalid Book record";

                    return BadRequest(response );

                }
                if (ModelState.IsValid)
                {
                    // This may not work - there will be books that have no series 
                    var series = _context.aSeries.Where(x => model.Series.Equals(x.Id)).FirstOrDefault();

                    if (series == null)
                    {
                        // TODO: figure out how to handle stand-alone books
                    }

                    var bookDetails = _context.Books.Include(x => x.Series).Where(x => x.Id == model.Id).FirstOrDefault();

                    if (bookDetails == null) 
                    {
                        response.Status = false;
                        response.Message = "Invalid Book record";

                        return BadRequest(response);
                    }

                    bookDetails.Name = model.Name;
                    bookDetails.AuthorFirst = model.AuthorFirst;
                    bookDetails.AuthorLast = model.AuthorLast;
                    bookDetails.ISBN = model.ISBN;
                    bookDetails.PublicationDate = model.PublicationDate;
                    bookDetails.Publisher = model.Publisher;
                    bookDetails.SeriesId = model.SeriesId;

                    _context.SaveChanges();

                    var responseData = new BookDetailsViewModel
                    {
                        Id = bookDetails.Id,
                        Name = bookDetails.Name,
                        AuthorFirst = bookDetails.AuthorFirst,
                        AuthorLast = bookDetails.AuthorLast,
                        SeriesId = bookDetails.SeriesId,
                        ISBN = bookDetails.ISBN,
                        PublicationDate = bookDetails.PublicationDate,
                        Publisher = bookDetails.Publisher
                    };

                    response.Status = true;
                    response.Message = "Created successfully";
                    response.Data = responseData;

                    return Ok(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Validation failed";
                    response.Data = ModelState;

                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest(response);
            }
        }
    }
}
