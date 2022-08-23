using Microsoft.AspNetCore.Mvc;

namespace DeweyHomeMovieApi
{
  [Route("api/[controller]")]
  [ApiController]
  public class MoviesController : ControllerBase
  {

    private static List<Movie> _movies = new List<Movie>();

    // GET: api/Movies
    [HttpGet]
    public IEnumerable<Movie> Get()
    {
      return new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Title = "The Shawshank Redemption",
                    FilePath = "C:\\Movies\\The Shawshank Redemption.mp4"
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Godfather",
                    FilePath = "C:\\Movies\\The Godfather.mp4"
                },
                new Movie
                {
                    Id = 3,
                    Title = "The Godfather: Part II",
                    FilePath = "C:\\Movies\\The Godfather: Part II.mp4"
                },
                new Movie
                {
                    Id = 4,
                    Title = "The Dark Knight",
                    FilePath = "C:\\Movies\\The Dark Knight.mp4"
                },
                new Movie
                {
                    Id = 5,
                }
            };
    }


    [HttpPost]
    public ActionResult Post([FromBody] Movie movie)
    {
      _movies.Add(movie);
      return Ok(movie);
    }
  }
}
