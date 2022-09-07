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

  [BsonRepresentation(BsonType.String)]
  public string? Title { get; set; }

  [BsonRepresentation(BsonType.String)]
  public string? Key { get; set; }

  [BsonRepresentation(BsonType.String)]
  public string? FilePath { get; set; }

  [BsonRepresentation(BsonType.String)]
  public string? Url { get; set; }

  [BsonRepresentation(BsonType.String)]
  public string? Length { get; set; }


  [BsonRepresentation(BsonType.Int32)]
  public int Version { get; set; } = 4;

  public IEnumerable<String>? Tags { get; set; }

  public IEnumerable<VideoTimeStamp>? VideoTimeStamps { get; set; }
}