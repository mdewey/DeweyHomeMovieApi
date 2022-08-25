using BookStoreApi.Services;
using DeweyHomeMovieApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeweyHomeMovieApi
{
  [Route("api/[controller]")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    private readonly MovieServices _movieService;

    public MoviesController(MovieServices service) =>
        _movieService = service;

    // GET: api/Movies
    [HttpGet]
    public async Task<ActionResult> Get()
    {
      return Ok(await this._movieService.Get());
    }

    // POST: api/Movies
    [HttpPost]
    public async Task<ActionResult> Post(Movie movie)
    {
      return Ok(await this._movieService.Insert(movie));
    }

  }
}
