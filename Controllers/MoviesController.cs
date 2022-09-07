using Amazon.S3;
using Amazon.S3.Model;
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
    private readonly IAmazonS3 _s3Client;

    private IConfiguration Configuration;

    public MoviesController(IConfiguration _configuration, MovieServices service, IAmazonS3 s3Client)
    {
      _s3Client = s3Client;
      _movieService = service;
      Configuration = _configuration;
    }
    // GET: api/Movies
    [HttpGet]
    public async Task<ActionResult> Get()
    {
      var movies = await this._movieService.Get();
      var rv = movies
                  .Select(s =>
                      {
                        var urlRequest = new GetPreSignedUrlRequest()
                        {
                          BucketName = Configuration["AWS:BucketName"],
                          Key = s.Key,
                          Expires = DateTime.UtcNow.AddMinutes(10),
                        };
                        return new
                        {
                          s.Title,
                          s.Length,
                          s.Tags,
                          s.Id,
                          s.Url,
                          s.Version,
                          s.VideoTimeStamps,
                          videoUrl = _s3Client.GetPreSignedURL(urlRequest)
                        };
                      });
      return Ok(rv);
    }


    // POST: api/Movies
    [HttpPost]
    public async Task<ActionResult> Post(Movie movie)
    {
      return Ok(await this._movieService.Insert(movie));
    }

  }
}
