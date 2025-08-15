using MediaTracker.Server.Data;
using MediaTracker.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                var bookList = _context.Books.Skip(pageIndex * pageSize).Take(pageSize).ToList();

                response.Status = true;
                response.Message = "Success";
                response.Data = new { Movies = bookList, Count = bookCount };

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
                var book = _context.Books.Where(x => x.BookId == id).FirstOrDefault();

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

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
