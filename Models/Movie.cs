using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeweyHomeMovieApi.Models;


public class Movie
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public string? Title { get; set; }

  public string? FilePath { get; set; }
}