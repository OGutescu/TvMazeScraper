using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TvMazeScraper.API.Models;
using TvMazeScraper.API.Repository;

namespace TvMazeScraper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TvShowsController : ControllerBase
    {
        private readonly IRepository _repository;

        public TvShowsController(IRepository repository)
        {
            _repository = repository;
        }

        // GET api/tvshows
        /// <summary>
        /// Gets a list of tv shows with their cast
        /// </summary>
        /// <param name="page">optional, default is 1 - is the page number</param>
        /// <param name="pageSize">optional, default is 25</param>
        /// <returns>
        /// 200 ok - list of tv shows with their cast
        /// 400 BadRequest - if the parameters are not valid
        /// 503 ServiceUnavailable - the db is not created
        /// 500 something went wrong with the call
        /// </returns>
        /// <example>
        ///     route: /api/tvshows
        ///     route: /api/tvshows/2
        ///     route: /api/tvshows/2/30
        /// Returns a json object like this:
        /// [{
        ///     "id": 1,
        ///     "name": "Under the Dome",
        ///     "cast": [{
        ///           "id": 7,
        ///           "name": "Mackenzie Lintz",
        ///           "birthday": "1996-11-22"
        ///       },
        ///       {
        ///           "id": 5,
        ///           "name": "Colin Ford",
        ///           "birthday": "1996-09-12"
        ///       }
        ///  }]
        /// </example>

        [HttpGet("{page?}/{pageSize?}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<TvShowModel>>> Get(int page = 1, int pageSize = 25)
        {
            if (page < 1 || page > 10000 || pageSize < 1 || pageSize > 1000)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Please provide page between [1..10000] and pageSize [1..1000]");
            }

            try
            {
                return await _repository.GetTvShowsPerPage(page, pageSize);
            }
            catch (SqlException sqlException)
            {
                return StatusCode((int)HttpStatusCode.ServiceUnavailable, "Please run the migrations - so the db is build. Then run the TvMazeScraper.Service so it brings data"); 
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something is wrong! Please contact Oana Gutescu for support :)");
            }
        }
    }
}