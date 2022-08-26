using DeweyHomeMovieApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class MovieServices
{
  private readonly IMongoCollection<Movie> _movieCollection;
  private readonly IMongoCollection<object> _testCollection;

  public MovieServices(
      IOptions<MoviesDatabaseSettings> settings)
  {
    var mongoClient = new MongoClient(
        settings.Value.ConnectionString);

    var mongoDatabase = mongoClient.GetDatabase(
        settings.Value.DatabaseName);

    _movieCollection = mongoDatabase.GetCollection<Movie>(
        settings.Value.MovieCollectionName);
    _testCollection = mongoDatabase.GetCollection<object>(
        settings.Value.TestCollectionName);

  }

  public async Task<List<object>> GetAllTestDocs()
  {
    return await _testCollection.Find(new BsonDocument()).ToListAsync();
  }

  public async Task<List<Movie>> Get()
  {
    return await _movieCollection.Find(new BsonDocument()).ToListAsync();
  }

  public async Task<Movie> Insert(Movie movie)
  {
    Console.WriteLine(movie.VideoTimeStamps.Count);
    await _movieCollection.InsertOneAsync(movie);
    return movie;
  }



}