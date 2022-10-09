using Amazon.S3;
using Amazon.S3.Model;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeweyHomeMovieApi
{
  [Route("api/[controller]")]
  [ApiController]
  public class TestController : ControllerBase
  {

    private readonly MovieServices _movieService;
    private readonly IAmazonS3 _s3Client;
    private readonly IConfiguration _configuration;

    public TestController(IConfiguration configuration, MovieServices service, IAmazonS3 s3Client)
    {
      _s3Client = s3Client;
      _movieService = service;
      _configuration = configuration;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
      return Ok(await this._movieService.GetAllTestDocs());
    }

    [HttpGet("ping")]
    public ActionResult Ping()
    {
      return Ok("pong");
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllBucketAsync()
    {
      var data = await _s3Client.ListBucketsAsync();
      var buckets = data.Buckets.Select(b => { return b.BucketName; });
      return Ok(buckets);
    }

    [HttpGet("get-env")]
    public ActionResult GetEnv()
    {
      return Ok(((IConfigurationRoot)_configuration).GetDebugView());
    }


    [HttpGet("get-configured-bucket")]
    public async Task<IActionResult> GetConfiguredBucketAsync()
    {
      var bucket = _configuration["AWS:BUCKET"];
      try
      {
        var data = await _s3Client.ListObjectsAsync(bucket);
        var buckets = data.S3Objects.Select(b => { return b.Key; });
        return Ok(new { bucket, buckets });
      }
      catch (Exception e)
      {
        return Ok(new { bucket, e.Message });
      }
    }

    public class S3ObjectDto
    {
      public string? Name { get; set; }
      public string? PresignedUrl { get; set; }
    }

    [HttpGet("get-all-full")]
    public async Task<IActionResult> GetAllFilesAsync(string bucketName = "deweys-home-video-bucket-dev", string? prefix = "xyz")
    {
      var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
      if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
      var request = new ListObjectsV2Request()
      {
        BucketName = bucketName,
        Prefix = prefix
      };
      var result = await _s3Client.ListObjectsV2Async(request);
      var s3Objects = result.S3Objects.Select(s =>
      {
        var urlRequest = new GetPreSignedUrlRequest()
        {
          BucketName = bucketName,
          Key = s.Key,
          Expires = DateTime.UtcNow.AddMinutes(1)
        };
        return new S3ObjectDto()
        {
          Name = s.Key.ToString(),
          PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
        };
      });
      return Ok(s3Objects);
    }

  }
}
