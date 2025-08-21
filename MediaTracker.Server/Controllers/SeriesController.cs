using MediaTracker.Server.Data;
using MediaTracker.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediaTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public SeriesController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(int pageIndex = 0, int pageSize = 10)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                var seriesCount = _context.aSeries.Count();
                var seriesList = _context.aSeries.Skip(pageIndex * pageSize).Take(pageSize).Select(x => new SeriesViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    IsCompleted = x.IsCompleted,
                    Length = x.Length
                }).ToList();

                response.Status = true;
                response.Message = "Success";
                response.Data = new { Series = seriesList, Count = seriesCount };

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
        public IActionResult GetSeriesById(int id)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                var series = _context.aSeries.Where(x => x.Id == id).FirstOrDefault();

                if (series == null)
                {
                    response.Status = false;
                    response.Message = "Series does not exist";

                    return BadRequest(response);
                }

                var seriesData = new SeriesDetailsViewModel
                {
                    Id = series.Id,
                    Name = series.Name,
                    Description = series.Description,
                    IsCompleted = series.IsCompleted,
                    Length = series.Length
                    //Books = _context.Books.Where(x => x.series.Contains(series)) TODO: Need to figure out this mess

                };

                response.Status = true;
                response.Message = "Success";
                response.Data = series;

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
        public IActionResult Post(SeriesViewModel model)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                if (ModelState.IsValid)
                {
                   var postedModel = new Entities.Series()
                    {
                        Name = model.Name,
                        Description= model.Description,
                        IsCompleted = model.IsCompleted,
                        Length = model.Length
                    };

                    _context.aSeries.Add(postedModel);
                    _context.SaveChanges();

                    model.Id = postedModel.Id;

                    response.Status = true;
                    response.Message = "Created successfully";
                    response.Data = model;

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
