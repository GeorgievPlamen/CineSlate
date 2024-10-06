using Api.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Movies;

public class MoviesController : BaseController
{
    [HttpGet]
    public IActionResult GetMovies()
    {
        return Ok("movies list");
    }
}