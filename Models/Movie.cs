using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeweyHomeMovieApi.Models;



public class VideoTimeStamp
{
  public string? Description { get; set; }
  public string? TimeStamp { get; set; }
}

public class Movie
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public string? Title { get; set; }

  public string? FilePath { get; set; }
  public string? Url { get; set; }

  public int Version { get; set; } = 1;

  public List<VideoTimeStamp>? VideoTimeStamps { get; set; }
}