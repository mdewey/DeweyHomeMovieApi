using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeweyHomeMovieApi
{
  [Route("api/[controller]")]
  [ApiController]
  public class TestController : ControllerBase
  {

    private readonly MovieServices _movieService;

    public TestController(MovieServices service) =>
        _movieService = service;

    // GET: api/Movies
    [HttpGet]
    public async Task<ActionResult> Get()
    {
      return Ok(await this._movieService.GetAllTestDocs());
    }

  }
}
